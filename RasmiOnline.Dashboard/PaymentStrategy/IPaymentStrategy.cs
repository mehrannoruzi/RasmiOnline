namespace RasmiOnline.Dashboard.PaymentStrategy
{
    using RasmiOnline.Domain.Dto;
    using RasmiOnline.Domain.Entity;
    using Gnu.Framework.Core;

    public interface IPaymentStrategy
    {
        IActionResponse<string> Do(PaymentGateway gateway, TransactionModel model);
        IActionResponse<string> Verify(PaymentGateway gateway, Transaction model, object responseGateway = null);
    }
}
