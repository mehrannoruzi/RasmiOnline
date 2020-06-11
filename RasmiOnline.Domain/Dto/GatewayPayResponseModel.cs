namespace RasmiOnline.Domain.Dto
{
    public class GatewayPayResponseModel
    {
        public int status { get; set; }
        public int transId { get; set; }
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
    }

    public class PayRedirectModel
    {
        public int status { get; set; }
        public int transId { get; set; }
        public long mobile { get; set; }
        public int factorNumber { get; set; }
        public string cardNumber { get; set; }
        public string message { get; set; }
    }
}