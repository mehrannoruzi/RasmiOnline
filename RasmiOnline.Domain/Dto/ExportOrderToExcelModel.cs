namespace RasmiOnline.Domain.Dto
{
    using System;
    using RasmiOnline.Domain.Enum;

    [Serializable]
    public class ExportOrderToExcelModel
    {
        public string OrderNumber { get; set; }
        public string OrderStatus { get; set; }
        public DeliveryType DeliverType { get; set; }
        public string DeliverFiles_DateSh { get; set; }
        public string DeliverFiles_DateMi { get; set; }
        public string InsertDateMi { get; set; }
        public string InsertDateSh { get; set; }
        public string OrderTitle { get; set; }
        public string OwnerFullName { get; set; }
        public string OwnerMobileNumber { get; set; }
        public string ReciverFullName { get; set; }
        public string ReciverMobileNumber { get; set; }
        public string ReciverLocation { get; set; }
        public string ReciverFullAddress { get; set; }

    }
}
