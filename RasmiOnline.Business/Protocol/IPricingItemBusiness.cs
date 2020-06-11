namespace RasmiOnline.Business.Protocol
{
    using Domain.Dto;
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Enum;
    using RasmiOnline.Domain.Entity;
    using System.Collections.Generic;

    public interface IPricingItemBusiness
    {
        PricingItem Find(int pricingItemId);
        IEnumerable<PricingItem> Get(bool isPricingItem);
        IEnumerable<PricingItem> Get(string str, bool? isPricingItem = null, int count = 50);
        IActionResponse<int> Insert(PricingItem model);
        IActionResponse<int> Update(PricingItem model);
        IActionResponse<bool> Delete(int pricingItemId);
        IActionResponse<PricingItem> GetPricingItem(int pricingItemId);
        IActionResponse<IEnumerable<PricingItem>> GetPricingItems(CalculatorTemplate templateId, int take = 0);
        IActionResponse<IEnumerable<PricingItem>> GetPricingItems();
        IActionResponse<List<PricingItem>> Search(FilterPricingItemModel filterModel);
        IEnumerable<PricingItem> GetMustlyUsed();
    }
}