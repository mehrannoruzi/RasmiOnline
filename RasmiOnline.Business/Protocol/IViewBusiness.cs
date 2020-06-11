namespace RasmiOnline.Business.Protocol
{
    using Gnu.Framework.Core;
    using Domain.Dto;
    using Domain.Entity;
    using System.Collections.Generic;

    public interface IViewBusiness
    {
        IActionResponse<int> Insert(View model);
        IActionResponse<int> Update(View model);
        IActionResponse<bool> Delete(int viewId);
        View Find(int viewId);
        IActionResponse<View> GetView(int viewId);
        IActionResponse<List<View>> Search(FilterViewModel filterModel);
        IList<ItemTextValueModel<string, int>> GetParentViews();
    }
}