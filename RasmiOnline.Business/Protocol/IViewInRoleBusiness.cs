namespace RasmiOnline.Business.Protocol
{
    using Gnu.Framework.Core;
    using Domain.Dto;
    using Domain.Entity;
    using System.Collections.Generic;

    public interface IViewInRoleBusiness
    {
        IActionResponse<int> Insert(ViewInRole model);
        IActionResponse<bool> Delete(int viewInRoleId);
        IActionResponse<IEnumerable<ViewInRole>> GetViewsInRole(int? roleId = null);
        IList<ItemTextValueModel<string, int>> GetFilteredViewsFullPath(int roleId);
    }
}