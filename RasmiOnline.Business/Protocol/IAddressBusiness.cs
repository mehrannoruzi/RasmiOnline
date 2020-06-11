namespace RasmiOnline.Business.Protocol
{
    using Domain.Entity;
    using Gnu.Framework.Core;
    using System;
    using System.Collections.Generic;

    public interface IAddressBusiness
    {
        IActionResponse<int> Insert(Address model);
        IActionResponse<int> Update(Address model);
        IActionResponse<int> Delete(int addressId);
        IEnumerable<Address> GetAll(Guid userId);
        IActionResponse<Address>  Find(Guid userId,int addressId);
    }
}
