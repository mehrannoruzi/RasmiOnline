namespace RasmiOnline.Business.Protocol
{
    using System;
    using Gnu.Framework.Core;
    using Domain.Entity;

    public interface IVerificationCodeBusiness
    {
        IActionResponse<VerificationCode> GetCode(Guid userId, string owner);
        IActionResponse<bool> VerifyCode(Guid userId, string code);
        IActionResponse<Guid> Verify(Guid userId);
    }
}
