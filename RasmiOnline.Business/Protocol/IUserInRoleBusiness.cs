namespace RasmiOnline.Business.Protocol
{
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Entity;
    using System.Collections.Generic;

    public interface IUserInRoleBusiness
    {
        IActionResponse<int> Insert(UserInRole model);
        IActionResponse<bool> Delete(int userInRoleId);
        IActionResponse<IEnumerable<UserInRole>> GetUserInRole(long mobileNumber);
        IActionResponse<string> GetUserInRoleChecked(int roleId, long mobileNumber);
    }
}