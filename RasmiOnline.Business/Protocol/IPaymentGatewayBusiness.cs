namespace RasmiOnline.Business.Protocol
{
    using Domain.Entity;
    using Gnu.Framework.Core;
    using System.Collections.Generic;

    public interface IPaymentGatewayBusiness
    {
        IActionResponse<int> Update(PaymentGateway model);
        IActionResponse<PaymentGateway> GetPaymentGateway(int paymentGatewayId);
        PaymentGateway Find(int id);
        IEnumerable<PaymentGateway> GetAll(bool? isActive = null);
    }
}
