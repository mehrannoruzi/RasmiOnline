namespace RasmiOnline.Domain.Dto
{
    public class ExportUserToExcelModel
    {
        public string MobileNumber { get; set; }
        public string IsActive { get; set; }
        public string RegisterDateMi { get; set; }
        public string LastLoginDateMi { get; set; }
        public string RegisterDateSh { get; set; }
        public string LastLoginDateSh { get; set; }
        public string NationalCode { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
    }
}
