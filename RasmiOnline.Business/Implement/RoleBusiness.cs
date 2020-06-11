namespace RasmiOnline.Business.Implement
{
    using System.Linq;
    using Gnu.Framework.Core;
    using System.Data.Entity;
    using RasmiOnline.Domain.Dto;
    using RasmiOnline.Domain.Entity;
    using RasmiOnline.Business.Protocol;
    using System.Collections.Generic;
    using RasmiOnline.Business.Properties;
    using Gnu.Framework.EntityFramework;
    using Gnu.Framework.EntityFramework.DataAccess;

    public class RoleBusiness : IRoleBusiness
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        readonly IDbSet<Role> _role;

        public RoleBusiness(IUnitOfWork uow)
        {
            _uow = uow;
            _role = _uow.Set<Role>();
        }
        #endregion

        public Role Find(int roleId) => _role.Find(roleId);

        public IActionResponse<int> Insert(Role model)
        {
            _role.Add(model);
            var result = _uow.SaveChanges();
            return new ActionResponse<int>
            {
                IsSuccessful = result.ToSaveChangeResult(),
                Message = result.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                Result = model.RoleId
            };
        }

        public IActionResponse<int> Update(Role model)
        {
            _role.Attach(model);
            _uow.Entry(model).State = EntityState.Modified;
            var result = _uow.SaveChanges();

            return new ActionResponse<int>
            {
                IsSuccessful = result.ToSaveChangeResult(),
                Message = result.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                Result = model.RoleId
            };
        }

        public IActionResponse<bool> Delete(int roleId)
        {
            var response = new ActionResponse<bool>();
            var role = Find(roleId);
            if (role == null)
            {
                response.Message = BusinessMessage.RecordNotFound;
                return response;
            }

            _uow.Set<Role>().Remove(role);
            var result = _uow.SaveChanges();

            response.Message = result.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error);
            response.IsSuccessful = result.ToSaveChangeResult();
            response.Result = response.IsSuccessful;
            return response;
        }


        public IActionResponse<Role> GetRole(int roleId)
        {
            var response = new ActionResponse<Role>();
            var result = _role.Find(roleId);
            if (result.IsNotNull())
            {
                response.IsSuccessful = true;
                response.Result = result;
            }
            else response.Message = BusinessMessage.RecordNotFound;
            return response;
        }

        public IActionResponse<IEnumerable<Role>> GetAll()
        {
            var response = new ActionResponse<IEnumerable<Role>>();
            response.Result = _role.Where(x => x.IsActive).AsNoTracking().OrderBy(x => x.RoleNameFa).ToList();

            if (response.Result.Any())
                response.IsSuccessful = true;
            else
                response.Message = BusinessMessage.RecordNotFound;

            return response;
        }

        public IActionResponse<List<Role>> Search(FilterRoleModel filterModel)
        {
            var response = new ActionResponse<List<Role>>();
            var result = _role.Where(x =>
                               (!string.IsNullOrEmpty(filterModel.RoleNameEn) ? x.RoleNameEn.Contains(filterModel.RoleNameEn) : true) &&
                               (!string.IsNullOrEmpty(filterModel.RoleNameFa) ? x.RoleNameFa.Contains(filterModel.RoleNameFa) : true) &&
                               (filterModel.IsActive != null ? x.IsActive == filterModel.IsActive : true) &&
                               (filterModel.IsDefault != null ? x.IsDefault == filterModel.IsDefault : true)
                         )
                         .OrderBy(x => x.RoleId)
                         .Select(s => s)
                         .Take(filterModel.ItemsCount)
                         .ToList();

            if (result.Any())
                response.IsSuccessful = true;
            else
                response.Message = BusinessMessage.UserNotFound;

            response.Result = result;
            return response;
        }
    }
}
