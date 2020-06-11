namespace RasmiOnline.Business.Implement
{
    using System;
    using System.Linq;
    using Gnu.Framework.Core;
    using System.Data.Entity;
    using System.Collections.Generic;
    using Gnu.Framework.EntityFramework;
    using Gnu.Framework.EntityFramework.DataAccess;
    using RasmiOnline.Domain.Entity;
    using Protocol;
    using RasmiOnline.Business.Properties;

    public class BankCardBusiness : IBankCardBusiness
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        readonly IDbSet<BankCard> _BankCard;

        public BankCardBusiness(IUnitOfWork uow)
        {
            _uow = uow;
            _BankCard = uow.Set<BankCard>();
        }
        #endregion


        /// <summary>
        /// Add BankCard With Task State in Processing
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResponse<int> Insert(BankCard model)
        {

            var findedCart = _BankCard.FirstOrDefault(X => X.CardNumber == model.CardNumber);
            if (findedCart != null)
                return new ActionResponse<int>
                {
                    Message = BusinessMessage.DuplicateRecord,
                };
            _BankCard.Add(model);
            var result = _uow.SaveChanges();

            return new ActionResponse<int>
            {
                IsSuccessful = result.ToSaveChangeResult(),
                Message = result.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                Result = model.BankCardId
            };
        }

        /// <summary>
        /// Update BankCard And With Task State in Processing
        /// If model.TaskState==Success this Method Must Reject
        /// </summary>
        /// <param name="model"></param>
        public IActionResponse<int> Update(BankCard model)
        {
            var response = new ActionResponse<int>();

            var findedBankCard = Find(model.BankCardId);
            if (!findedBankCard.IsSuccessful)
                response.Message = findedBankCard.Message;
            else
            {

                findedBankCard.Result.UpdateWith(new
                {
                    model.AccountNumber,
                    model.BankName,
                    model.CardNumber,
                    model.IsEnable
                });
                _uow.Entry(findedBankCard.Result).State = EntityState.Modified;
                response.Result = _uow.SaveChanges();

                response.IsSuccessful = response.Result.ToSaveChangeResult();
                response.Message = response.Result.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error);
            }
            return response;
        }

        /// <summary>
        /// Delete BankCard 
        /// If model.TaskState != Success
        /// </summary>
        /// <param name="BankCardId"></param>
        /// <returns></returns>
        public IActionResponse<bool> Delete(int BankCardId)
        {
            var response = new ActionResponse<bool>();
            var findedBankCard = _BankCard.Find(BankCardId);
            if (findedBankCard.IsNull())
            {
                response.Message = BusinessMessage.RecordNotFound;
                return response;
            }

            _uow.Set<BankCard>().Remove(findedBankCard);
            var result = _uow.SaveChanges();

            if (result <= 0)
                response.Message = BusinessMessage.Error;
            else
                response.Result = response.IsSuccessful = true;

            return response;
        }

        /// <summary>
        /// Get All BankCard Per UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<BankCard> GetAll() => _BankCard.AsNoTracking().ToList();

        /// <summary>
        /// Get All BankCard Per UserId
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<BankCard> GetAll(bool isEnable) => _BankCard.Where(X => X.IsEnable == isEnable).AsNoTracking().ToList();

        /// <summary>
        /// Find UnDeleted BankCard with BankCardID
        /// </summary>
        /// <param name="BankCardId">BankCardID</param>
        /// <returns></returns>
        public IActionResponse<BankCard> Find(int BankCardId)
        {
            var response = new ActionResponse<BankCard>();
            var BankCard = _BankCard.FirstOrDefault(x => !x.IsEnable && x.BankCardId == BankCardId);

            if (BankCard.IsNull()) response.Message = BusinessMessage.RecordNotFound;
            else
            {
                response.Result = BankCard;
                response.IsSuccessful = true;
            }
            return response;
        }
    }
}
