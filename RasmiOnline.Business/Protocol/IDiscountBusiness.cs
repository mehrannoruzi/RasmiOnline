namespace RasmiOnline.Business.Protocol
{
    using Domain.Dto;
    using Domain.Entity;
    using Gnu.Framework.Core;
    using System.Collections.Generic;

    public interface IDiscountBusiness
    {
        IActionResponse<bool> Generate(AddDiscountModel model);
        IActionResponse<bool> Use(string code);
        IActionResponse<Discount> Get(string code);
        IActionResponse<Discount> Get(int id);
        IActionResponse<List<Discount>> GetList(int count = 100);
        IActionResponse<Discount> Check(DiscountCheckModel model);
    }
}
