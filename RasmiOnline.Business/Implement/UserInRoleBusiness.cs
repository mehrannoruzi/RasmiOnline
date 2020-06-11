namespace RasmiOnline.Business.Implement
{
    using System;
    using System.Linq;
    using System.Data;
    using Gnu.Framework.Core;
    using System.Data.Entity;
    using RasmiOnline.Domain.Entity;
    using System.Collections.Generic;
    using RasmiOnline.Business.Protocol;
    using RasmiOnline.Business.Properties;
    using Gnu.Framework.EntityFramework;
    using Gnu.Framework.EntityFramework.DataAccess;

    public class UserInRoleBusiness : IUserInRoleBusiness
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        readonly IDbSet<UserInRole> _userInRole;
        readonly IRoleBusiness _roleBusiness;
        readonly Lazy<IUserBusiness> _lazyUserBusiness;

        public UserInRoleBusiness(IUnitOfWork uow, IRoleBusiness roleBusiness, Lazy<IUserBusiness> lazyUserBusiness)
        {
            _uow = uow;
            _userInRole = _uow.Set<UserInRole>();
            _roleBusiness = roleBusiness;
            _lazyUserBusiness = lazyUserBusiness;
        }

        #endregion

        public IActionResponse<int> Insert(UserInRole model)
        {
            var currentDate = DateTime.Now.Date;
            var userInRole = _userInRole.FirstOrDefault(x=> x.RoleId == model.RoleId && x.UserId == model.UserId && x.ExpireDateMi >= currentDate);

            if (userInRole.IsNotNull())
                return new ActionResponse<int>
                {
                    Message = BusinessMessage.CanNotAddNewRoleDueToActiveRole
                };

            model.ExpireDateMi = PersianDateTime.Parse(model.ExpireDateSh).ToDateTime();
            _userInRole.Add(model);
            var result = _uow.SaveChanges();
            return new ActionResponse<int>
            {
                IsSuccessful = result.ToSaveChangeResult(),
                Message = result.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                Result = model.UserInRoleId
            };
        }

        public IActionResponse<bool> Delete(int userInRoleId)
        {
            var response = new ActionResponse<bool>();
            var userInRole = _userInRole.Find(userInRoleId);
            if (userInRole == null)
            {
                response.Message = BusinessMessage.RecordNotFound;
                return response;
            }

            _uow.Set<UserInRole>().Remove(userInRole);
            var result = _uow.SaveChanges();

            response.Message = result.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error);
            response.IsSuccessful = result.ToSaveChangeResult();
            response.Result = response.IsSuccessful;
            return response;
        }


        public IActionResponse<IEnumerable<UserInRole>> GetUserInRole(long mobileNumber)
        {
            var response = new ActionResponse<IEnumerable<UserInRole>>();

            var user = _lazyUserBusiness.Value.Find(mobileNumebr: mobileNumber);

            if (user.IsNull())
            { 
                response.Message = BusinessMessage.UserNotFound;
                return response;
            }

            var result = _userInRole.Include(i => i.Role)
                         .Include(i => i.User)
                         .Where(x => x.UserId == user.UserId)
                         .OrderByDescending(x => x.UserInRoleId)
                         .ToList();

            if (result.Any())
                response.IsSuccessful = true;
            else
                response.Message = BusinessMessage.UserNotFound;

            response.Result = result;
            return response;
        }

        public IActionResponse<string> GetUserInRoleChecked(int roleId, long mobileNumber)
        {
            var response = new ActionResponse<string>();

            var role = _roleBusiness.Find(roleId);
            if (role.IsNull())
            {
                response.Message = BusinessMessage.InvalidRole;
                return response;
            }

            var user = _lazyUserBusiness.Value.Find(mobileNumebr: mobileNumber);
            if (user.IsNull())
            {
                response.Message = BusinessMessage.UserNotFound;
                return response;
            }

            response.IsSuccessful = true;
            response.Result = user.FullName;

            return response;
        }
    }
}