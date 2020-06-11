namespace RasmiOnline.Business.Implement
{
    using System;
    using Protocol;
    using Domain.Dto;
    using Properties;
    using System.Linq;
    using System.Text;
    using Domain.Entity;
    using System.Data.Entity;
    using Gnu.Framework.Core;
    using System.Data.SqlClient;
    using Gnu.Framework.AspNet.Cache;
    using System.Collections.Generic;
    using Gnu.Framework.EntityFramework;
    using Gnu.Framework.Core.Authentication;
    using Gnu.Framework.EntityFramework.DataAccess;
    using Domain.Enum;
    using Observers;
    using Gnu.Framework.Core.Security;

    public class UserBusiness : IUserBusiness, IExportTableBusiness
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        readonly IDbSet<User> _user;
        readonly ICacheProvider _cache;
        readonly Lazy<IObserverManager> _observerManager;
        public UserBusiness(IUnitOfWork uow, ICacheProvider cache, Lazy<IObserverManager> observerManager)
        {
            _uow = uow;
            _observerManager = observerManager;
            _user = _uow.Set<User>();
            _cache = cache;
        }
        #endregion

        public User Find(Guid userId) => _user.AsNoTracking().FirstOrDefault(x => x.UserId == userId);
        public User Find(string username) => _user.AsNoTracking().FirstOrDefault(x => x.Email == username);
        public User Find(long mobileNumebr) => _user.FirstOrDefault(x => x.MobileNumber == mobileNumebr);

        /// <summary>
        /// Update Given User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResponse<User> Update(User model)
        {
            _uow.Entry(model).State = EntityState.Modified;
            var result = _uow.SaveChanges();
            return new ActionResponse<User>
            {
                IsSuccessful = result.ToSaveChangeResult(),
                Message = result.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                Result = model,
            };

        }

        public IActionResponse<User> Activate(Guid userId)
        {
            var user = _user.Find(userId);
            if (user == null)
                return new ActionResponse<User>
                {
                    IsSuccessful = false,
                    Message = BusinessMessage.RecordNotFound
                };
            user.IsActive = true;
            return Update(user);
        }

        public IActionResponse<bool> Update(PersonalInfo model)
        {
            var findedUser = _user.Find(model.UserId);
            if (findedUser == null)
                return new ActionResponse<bool>()
                {
                    IsSuccessful = false,
                    Message = BusinessMessage.RecordNotFound
                };

            findedUser.NationalCode = model.NationalCode;
            findedUser.FirstName = model.FirstName;
            findedUser.LastName = model.LastName;
            findedUser.MobileNumber = model.MobileNumber;
            findedUser.Email = model.Email;
            findedUser.IsActive = model.IsActive;

            _uow.Entry(findedUser).State = EntityState.Modified;
            var saveChange = _uow.SaveChanges();
            return new ActionResponse<bool>()
            {
                IsSuccessful = saveChange.ToSaveChangeResult(),
            };
        }

        /// <summary>
        /// signup method
        /// </summary>
        /// <param name="user"></param>
        /// <param name="RoleId"></param>
        /// <param name="levelId"></param>
        /// <returns></returns>
        public IActionResponse<Guid> InsertCustomer(User user, int RoleId)
        {
            user.RegisterDateMi = DateTime.Now;
            user.RegisterDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);
            user.LastLoginDateMi = user.RegisterDateMi;
            user.LastLoginDateSh = user.RegisterDateSh;
            user.IsActive = true;
            user.UserId = Guid.NewGuid();
            user.Password = HashGenerator.Hash(user.Password.Trim(), HashAlgorithms.SHA1);
            if (_user.Any(x => x.Email == user.Email))
                return new ActionResponse<Guid>
                {
                    IsSuccessful = false,
                    Message = BusinessMessage.UsernameExist
                };
            _user.Add(user);
            _uow.Set<UserInRole>().Add(new UserInRole
            {
                IsActive = true,
                UserId = user.UserId,
                RoleId = RoleId,
                ExpireDateMi = DateTime.Now.AddYears(10),
                ExpireDateSh = PersianDateTime.Now.AddYears(10).ToString(PersianDateTimeFormat.Date)
            });
            var result = _uow.SaveChanges();
            if (result.ToSaveChangeResult())
            {
                _observerManager.Value.Notify(ConcreteKey.User_Register, new ObserverMessage
                {
                    BotContent = string.Format(BusinessMessage.User_Register, $"{user.FirstName} {user.LastName}", $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}"),
                    UserId = user.UserId,
                    Key = $"{ConcreteKey.User_Register}|{user.UserId}",
                });
            }
            return new ActionResponse<Guid>
            {
                Result = user.UserId,
                IsSuccessful = result.ToSaveChangeResult(),
                Message = result.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error)
            };
        }

        /// <summary>
        /// Check User Credentials For login
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns></returns>
        public IActionResponse<User> CheckUserCredentials(string email, string password)
        {
             password = HashGenerator.Hash(password.Trim(), HashAlgorithms.SHA1);
            var user = _user.FirstOrDefault(x => x.Email == email && x.Password == password);
            if (user == null) return new ActionResponse<User> { Message = BusinessMessage.WrongUsernameOrPassword, IsSuccessful = false };
            else if (!user.IsActive) return new ActionResponse<User> { Message = BusinessMessage.InActiveUser, IsSuccessful = false };

            user.LastLoginDateMi = DateTime.Now;
            _uow.SaveChanges();
            return new ActionResponse<User> { IsSuccessful = true, Result = user };
        }

        public IEnumerable<MenuSPModel> ExecuteMenuSP(Guid userId)
            => _uow.Database.SqlQuery<MenuSPModel>("[Acl].[GetUserMenu] @UserId",
                new SqlParameter("@UserId", userId)
                {
                    SqlDbType = System.Data.SqlDbType.UniqueIdentifier
                }).ToList();

        /// <summary>
        /// get list of defined views for user and cache it
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="spResult"></param>
        /// <param name="ignoreCache"></param>
        /// <returns>model that maps the sp result</returns>
        public MenuModel GetAvailableActions(Guid userId, List<MenuSPModel> spResult = null)
        {
            var key = $"UserActions_{userId.ToString().Replace("-", "_")}";
            var menuModel = (MenuModel)_cache.GetItem(key);
            if (menuModel != null)
                return menuModel;
            if (spResult == null) spResult = ExecuteMenuSP(userId).ToList();

            #region Find Default View
            var defaultAction = new CustomUserAction();
            foreach (var menuItem in spResult)
            {
                var views = menuItem.ViewsList;
                if (views.Any(x => x.IsDefault))
                {
                    defaultAction = new CustomUserAction
                    {
                        Action = views.FirstOrDefault(x => x.IsDefault).ActionName,
                        Controller = views.FirstOrDefault(x => x.IsDefault).Controller,
                        RoleId = views.FirstOrDefault(x => x.IsDefault).RoleId
                    };
                    break;
                }
            }
            if (defaultAction.Controller.IsNull()) return null;
            #endregion

            var userActions = (spResult.Where(x => x.IsView).Select(rvm => new UserAction
            {
                Controller = rvm.Controller.ToLower(),
                Action = rvm.ActionName.ToLower(),
            })
             .Union(
                 spResult.Where(x => !x.IsView).SelectMany(x => x.ViewsList.Select(rvm => new UserAction
                 {
                     Controller = rvm.Controller.ToLower(),
                     Action = rvm.ActionName.ToLower()
                 })))).ToList();
            menuModel = GetAvailableMenu(new MenuModel
            {
                UserId = userId,
                SpResult = spResult,
                Items = userActions,
                DefaultUserAction = defaultAction
            });
            _cache.PutItemSliding(key, menuModel, null, 30);
            return menuModel;

        }

        /// <summary>
        /// get & cache available dashboard side bar menu 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="currentUrl"></param>
        /// <param name="menu"></param>
        /// <returns></returns>
        private MenuModel GetAvailableMenu(MenuModel menuModel)
        {
            var key = $"Menu_{menuModel.UserId.ToString().Replace("-", "_")}";
            var availableMenu = (string)_cache.GetItem(key);
            if (availableMenu != null)
            {
                menuModel.Menu = availableMenu;
                return menuModel;
            }

            var sb = new StringBuilder(string.Empty);

            var tempList = new List<UserAction>();
            foreach (var item in menuModel.SpResult.Where(x => x.IsVisible && (x.IsView || (!x.IsView && x.ViewsList.Any(v => v.IsVisible)))))
            {
                sb.AppendFormat("<li class='{0}'><a href='{1}'><i class='{2}'></i>{3}</a>",
                    item.IsView ? "" : "navigation__sub variantsactive",
                    item.IsView ? $"/{item.Controller.ToLower()}/{item.ActionName.ToLower()}" : "#",
                    item.Icon,
                    item.ActionNameFa);

                if (!item.IsView)
                {
                    sb.Append("<ul class='submenu'>");
                    foreach (var v in item.ViewsList.Where(x => x.IsVisible))
                    {
                        sb.AppendFormat("<li class=''><a href='{0}'><i class='{1}'></i>{2}</a><li>",
                        $"/{v.Controller.ToLower()}/{v.ActionName.ToLower()}",
                        v.Icon,
                        v.ActionNameFa);
                    }
                    sb.Append("</ul>");
                }
                sb.Append("</li>");
            }
            availableMenu = sb.ToString();
            _cache.PutItemSliding(key, availableMenu, null, 30);
            menuModel.Menu = availableMenu;
            return menuModel;
        }

        public IActionResponse<List<UserDetailsModel>> Search(FilterUserModel filterModel)
        {
            var response = new ActionResponse<List<UserDetailsModel>>();
            var result = _user.Where(x =>
                               (!string.IsNullOrEmpty(filterModel.FirstName) ? x.FirstName.Contains(filterModel.FirstName) : true) &&
                               (!string.IsNullOrEmpty(filterModel.LastName) ? x.LastName.Contains(filterModel.LastName) : true) &&
                               (!string.IsNullOrEmpty(filterModel.NationalCode) ? x.NationalCode == filterModel.NationalCode : true) &&
                               (filterModel.MobileNumber != null ? x.MobileNumber == filterModel.MobileNumber : true) &&
                               (filterModel.IsActive != null ? x.IsActive == filterModel.IsActive : true)
                         )
                         .OrderByDescending(x => x.RegisterDateMi)
                         .Select(s => new UserDetailsModel
                         {
                             MobileNumber = s.MobileNumber,
                             NationalCode = s.NationalCode,
                             IsActive = s.IsActive,
                             UserId = s.UserId,
                             FullName = s.FirstName + " " + s.LastName,
                             LastLoginDateMi = s.LastLoginDateMi,
                             RegisterDateMi = s.RegisterDateMi,
                             Email = s.Email
                         })
                         .Take(filterModel.ItemsCount)
                         .ToList();

            if (result.Any())
                response.IsSuccessful = true;
            else
                response.Message = BusinessMessage.UserNotFound;

            response.Result = result;
            return response;
        }

        public IActionResponse<FileExportResultModel> ExportExcelFile(ExportDataTableModel filterModel)
        {
            var response = new ActionResponse<FileExportResultModel>();
            var result = (from user in _user.AsNoTracking()
                          where user.RegisterDateMi >= filterModel.DateTimeFrom && user.RegisterDateMi <= filterModel.DateTimeTo
                          orderby user.RegisterDateMi descending
                          select new ExportUserToExcelModel
                          {
                              FullName = user.FirstName + " " + user.LastName,
                              IsActive = user.IsActive ? "Yes" : "No",
                              LastLoginDateMi = user.LastLoginDateMi.ToString(),
                              LastLoginDateSh = user.LastLoginDateSh,
                              RegisterDateSh = user.RegisterDateSh,
                              MobileNumber = user.MobileNumber.ToString(),
                              NationalCode = user.NationalCode,
                              RegisterDateMi = user.RegisterDateMi.ToString(),
                              Username = user.Email
                          }).ToList();

            if (result.IsNotNull().And(result.Any()))
            {
                response.IsSuccessful = true;
                response.Result = new FileExportResultModel { FileResult = result, RecordsCount = result.Count };
            }
            else response.Message = BusinessMessage.RecordNotFound;
            return response;
        }

        public IActionResponse<string> ChangeCurrentPassword(Guid userId, string currentPassword, string newPassword)
        {
            var user = _user.Find(userId);
            if (user == null) return new ActionResponse<string> { Message = BusinessMessage.UserNotFound };
            currentPassword = HashGenerator.Hash(currentPassword.Trim(), HashAlgorithms.SHA1);
            if (user.Password != currentPassword) return new ActionResponse<string> { Message = BusinessMessage.WrongPassword };
            user.Password = HashGenerator.Hash(newPassword.Trim(), HashAlgorithms.SHA1);
            _uow.Entry(user).Property(x => x.Password).IsModified = true;
            var res = _uow.SaveChanges();
            return new ActionResponse<string>
            {
                Result = user.Email,
                IsSuccessful = res.ToSaveChangeResult(),
                Message = res.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error)
            };
        }

        public IActionResponse<string> ChangeUserPassword(Guid userId, string newPassword)
        {
            var user = _user.Find(userId);
            if (user.IsNull()) return new ActionResponse<string> { Message = BusinessMessage.UserNotFound };
            user.Password = HashGenerator.Hash(newPassword.Trim(), HashAlgorithms.SHA1);
            _uow.Entry(user).Property(x => x.Password).IsModified = true;
            var res = _uow.SaveChanges();
            return new ActionResponse<string>
            {
                Result = user.Email,
                IsSuccessful = res.ToSaveChangeResult(),
                Message = res.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error)
            };
        }

        public IActionResponse<Guid> Insert(AddUserModel model)
        {
            //if (Find(Convert.ToInt64(model.MobileNumber)) != null)
            //    return new ActionResponse<Guid> { Message = BusinessMessage.MobileNumberIsAlreadyRegistered };

            if (_user.Any(x => x.Email == model.Email))
                return new ActionResponse<Guid>
                {
                    IsSuccessful = false,
                    Message = BusinessMessage.UsernameExist
                };

            var newUser = new User
            {
                Email = model.Email,
                MobileNumber = Convert.ToInt64(model.MobileNumber),
                FirstName = model.FirstName,
                IsActive = model.IsActive,
                LastName = model.LastName,
                NationalCode = model.NationalCode,
                Password = HashGenerator.Hash(model.Password.Trim(), HashAlgorithms.SHA1),
                LastLoginDateMi = DateTime.Now,
                LastLoginDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date),
                RegisterDateMi = DateTime.Now,
                RegisterDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date)
            };

            _uow.Entry(newUser).State = EntityState.Added;
            var res = _uow.SaveChanges();

            _uow.Set<UserInRole>().Add(new UserInRole
            {
                IsActive = true,
                ExpireDateMi = DateTime.Now.AddYears(2),
                ExpireDateSh = PersianDateTime.Now.AddYears(2).ToString(PersianDateTimeFormat.Date),
                RoleId = model.RoleId,
                UserId = newUser.UserId
            });

            res = _uow.SaveChanges();

            return new ActionResponse<Guid>
            {
                IsSuccessful = res.ToSaveChangeResult(),
                Result = newUser.UserId,
                Message = res.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error)
            };
        }

        public IEnumerable<ReferralModel> GetReferralUser(Guid userId)
        {
            var restul = _uow.Database.SqlQuery<ReferralModel>("[dbo].[GetReferralUser] @UserId",
                new SqlParameter("@UserId", userId)
                {
                    SqlDbType = System.Data.SqlDbType.UniqueIdentifier
                }).ToList();
            return restul;
        }

    }
}