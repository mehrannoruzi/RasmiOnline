namespace RasmiOnline.Business.Implement
{
    using System;
    using Protocol;
    using Properties;
    using System.Linq;
    using Domain.Entity;
    using SharedPreference;
    using System.Data.Entity;
    using Gnu.Framework.Core;
    using Gnu.Framework.Core.Log;
    using Gnu.Framework.EntityFramework;
    using Gnu.Framework.EntityFramework.DataAccess;

    public class VerificationCodeBusiness : IVerificationCodeBusiness
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        readonly IDbSet<User> _user;
        readonly IDbSet<VerificationCode> _verificationCode;

        public VerificationCodeBusiness(IUnitOfWork uow)
        {
            _uow = uow;
            _user = uow.Set<User>();
            _verificationCode = uow.Set<VerificationCode>();
        }
        #endregion

        public IActionResponse<VerificationCode> GetCode(Guid userId, string owner)
        {
            var result = new ActionResponse<VerificationCode>();
            try
            {
                var verificationCode = new VerificationCode { UserId = userId, IsUsed = false, Owner = owner, Code = Randomizer.RandomInteger(6).ToString() };
                _verificationCode.Add(verificationCode);
                var saveChange = _uow.SaveChanges();

                result.IsSuccessful = saveChange.ToSaveChangeResult();
                result.Message = saveChange.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error);
                result.Result = verificationCode;
                return result;
            }
            catch (Exception e)
            {
                FileLoger.Error(e, GlobalVariable.LogPath);
                result.Message = BusinessMessage.Exception;
                return result;
            }
        }

        public IActionResponse<bool> VerifyCode(Guid userId, string code)
        {
            var result = new ActionResponse<bool>();
            try
            {
                DateTime expireTime = DateTime.Now.AddMinutes(-20);
                var verificationCode = _verificationCode.FirstOrDefault(x => !x.IsUsed && x.Code == code && x.InsertDateMi >= expireTime);
                if (verificationCode.IsNull())
                {
                    result.Message = BusinessMessage.InvalidInputModelData;
                }
                else
                {
                    Verify(verificationCode.UserId);
                    verificationCode.IsUsed = true;

                    _uow.SaveChanges();

                    result.Result = true;
                    result.IsSuccessful = true;
                    result.Message = BusinessMessage.Success;
                }
                return result;
            }
            catch (Exception e)
            {
                FileLoger.Error(e, GlobalVariable.LogPath);
                result.Message = BusinessMessage.Exception;
                return result;
            }
        }

        public IActionResponse<Guid> Verify(Guid userId)
        {
            var response = new ActionResponse<Guid>();
            var findedUser = _user.Find(userId);
            if (findedUser.IsNull())
                response.Message = BusinessMessage.UserNotFound;
            else
            {
                findedUser.IsActive = true;
                _uow.Entry(findedUser).State = EntityState.Modified;
                var result = _uow.SaveChanges();
                response.Result = userId;
                response.IsSuccessful =  result.ToSaveChangeResult();
                response.Message = result.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error);
            }
            return response;
        }
    }
}
