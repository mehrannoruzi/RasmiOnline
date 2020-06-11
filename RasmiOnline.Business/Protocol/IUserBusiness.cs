namespace RasmiOnline.Business.Protocol
{
    using Gnu.Framework.Core;
    using Domain.Dto;
    using Domain.Entity;
    using System;
    using System.Collections.Generic;

    public interface IUserBusiness
    {
        IActionResponse<Guid> Insert(AddUserModel model);
        User Find(Guid userId);
        User Find(string username);
        User Find(long mobileNumebr);
        IActionResponse<User> Update(User model);
        IActionResponse<bool> Update(PersonalInfo model);
        IActionResponse<User> Activate(Guid userId);
        IActionResponse<Guid> InsertCustomer(User model, int RoleId);
        IActionResponse<User> CheckUserCredentials(string email, string password);
        IEnumerable<MenuSPModel> ExecuteMenuSP(Guid userId);
        MenuModel GetAvailableActions(Guid userId, List<MenuSPModel> menu = null);
        IActionResponse<List<UserDetailsModel>> Search(FilterUserModel filterModel);
        IActionResponse<string> ChangeCurrentPassword(Guid userId, string currentPassword, string newPassword);
        IActionResponse<string> ChangeUserPassword(Guid userId, string newPassword);
        IEnumerable<ReferralModel> GetReferralUser(Guid userId);
    }
}
