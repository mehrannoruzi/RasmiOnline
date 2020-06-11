namespace RasmiOnline.Business.Implement
{
    using Protocol;
    using System.Linq;
    using Domain.Entity;
    using System.Data.Entity;
    using System.Collections.Generic;
    using Gnu.Framework.EntityFramework.DataAccess;
    using Gnu.Framework.Core;
    using System;
    using Properties;
    using Gnu.Framework.EntityFramework;
    using Domain.Enum;

    public class PaymentGatewayBusiness : IPaymentGatewayBusiness
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        readonly IDbSet<PaymentGateway> _paymentGateway;
        public PaymentGatewayBusiness(IUnitOfWork uow)
        {
            _uow = uow;
            _paymentGateway = uow.Set<PaymentGateway>();
        }
        #endregion

        public IActionResponse<int> Update(PaymentGateway model)
        {
            var response = new ActionResponse<int>();

            var findedPaymentGateway = GetPaymentGateway(model.PaymentGatewayId);
            if (!findedPaymentGateway.IsSuccessful)
                response.Message = findedPaymentGateway.Message;
            else
            {
                findedPaymentGateway.Result.UpdateWith(new
                {
                    model.BankName,
                    model.IsActive,
                    model.MerchantId,
                    model.Name,
                    model.Password,
                    model.Username
                });

                _uow.Entry(findedPaymentGateway.Result).State = EntityState.Modified;

                response.Result = _uow.SaveChanges();
                response.IsSuccessful = response.Result.ToSaveChangeResult();
                response.Message = response.Result.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error);
            }
            return response;
        }

        public IActionResponse<PaymentGateway> GetPaymentGateway(int id)
        {
            var response = new ActionResponse<PaymentGateway>()
            {
                IsSuccessful = true
            };
            var result = _paymentGateway.Find(id);
            if (result == null)
            {
                response.IsSuccessful = false;
                response.Message = BusinessMessage.RecordNotFound;
            }
            response.Result = result;
            return response;
        }

        public PaymentGateway Find(int id) => _paymentGateway.Find(id);

        /// <summary>
        /// Get All Active PaymentGateway  
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<PaymentGateway> GetAll(bool? isActive = null)
            => _paymentGateway
               .Where(X => (isActive != null ? X.IsActive == isActive : true))
               .AsNoTracking()
               .ToList();
    }
}
