namespace RasmiOnline.Business.Protocol
{
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Dto;
    using RasmiOnline.Domain.Entity;
    using System.Collections.Generic;

    public interface IRoleBusiness
    {
        IActionResponse<int> Insert(Role model);
        IActionResponse<int> Update(Role model);
        IActionResponse<bool> Delete(int roleId);
        Role Find(int roleId);
        IActionResponse<Role> GetRole(int roleId);
        IActionResponse<IEnumerable<Role>> GetAll();
        IActionResponse<List<Role>> Search(FilterRoleModel filterModel);
    }
}