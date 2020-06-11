namespace RasmiOnline.Business.Implement
{
    using System.Linq;
    using System.Data;
    using Gnu.Framework.Core;
    using System.Data.Entity;
    using RasmiOnline.Domain.Dto;
    using System.Data.SqlClient;
    using RasmiOnline.Domain.Entity;
    using System.Collections.Generic;
    using RasmiOnline.Business.Protocol;
    using RasmiOnline.Business.Properties;
    using Gnu.Framework.EntityFramework;
    using Gnu.Framework.EntityFramework.DataAccess;

    public class ViewInRoleBusiness: IViewInRoleBusiness
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        readonly IDbSet<ViewInRole> _viewInRole;

        public ViewInRoleBusiness(IUnitOfWork uow)
        {
            _uow = uow;
            _viewInRole = _uow.Set<ViewInRole>();
        }
        #endregion
        
        public IActionResponse<int> Insert(ViewInRole model)
        {
            model.ExpireDateMi = PersianDateTime.Parse(model.ExpireDateSh).ToDateTime();
            _viewInRole.Add(model);
            var result = _uow.SaveChanges();
            return new ActionResponse<int>
            {
                IsSuccessful = result.ToSaveChangeResult(),
                Message = result.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                Result = model.ViewInRoleId
            };
        }

        public IActionResponse<bool> Delete(int viewInRoleId)
        {
            var response = new ActionResponse<bool>();
            var viewInRole = _viewInRole.Find(viewInRoleId);
            if (viewInRole == null)
            {
                response.Message = BusinessMessage.RecordNotFound;
                return response;
            }

            _uow.Set<ViewInRole>().Remove(viewInRole);
            var result = _uow.SaveChanges();

            response.Message = result.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error);
            response.IsSuccessful = result.ToSaveChangeResult();
            response.Result = response.IsSuccessful;
            return response;
        }

        public IActionResponse<IEnumerable<ViewInRole>> GetViewsInRole(int? roleId = null)
        {
            var response = new ActionResponse<IEnumerable<ViewInRole>>();

            var result = _viewInRole.Include(i => i.Role)
                         .Include(i => i.View)
                         .Where(x => (roleId != null ? x.RoleId == roleId : true))
                         .OrderByDescending(x => x.ViewInRoleId)
                         .ToList();

            if (result.Any())
                response.IsSuccessful = true;
            else
                response.Message = BusinessMessage.UserNotFound;

            response.Result = result;
            return response;
        }

        public IList<ItemTextValueModel<string, int>> GetFilteredViewsFullPath(int roleId)
        =>
            _uow.Database.SqlQuery<ViewsFullPathModel>("[Acl].[GetFilteredViewsFullPath] @RoleId",
                new SqlParameter("@RoleId", roleId)
                {
                    SqlDbType = SqlDbType.Int
                }).ToList()
                  .Select(x => new ItemTextValueModel<string, int>
                  {
                      Value = x.ViewId,
                      Key = x.FullPath
                  }).ToList();
    }
}