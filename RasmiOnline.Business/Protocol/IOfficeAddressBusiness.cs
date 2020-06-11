namespace RasmiOnline.Business.Protocol
{
    using Domain.Enum;
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Entity;
    using System.Collections.Generic;

    public interface IOfficeAddressBusiness
    {
        IActionResponse<int> Insert(OfficeAddress model);
        IActionResponse<int> Update(OfficeAddress model);
        IActionResponse<bool> ChangeActiveStatus(int officeAddressId);
        IActionResponse<IEnumerable<OfficeAddress>> GetAll();
        IActionResponse<OfficeAddress> Find(int officeAddressId);
        OfficeAddress Get(LangType langType);
    }
}