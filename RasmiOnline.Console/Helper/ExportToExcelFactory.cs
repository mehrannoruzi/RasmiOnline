namespace RasmiOnline.Console
{
    using System;
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Dto;
    using RasmiOnline.Domain.Enum;
    using RasmiOnline.Business.Protocol;
    using RasmiOnline.Business.Implement;
    using RasmiOnline.Console.Properties;

    public class ExportToExcelFactory
    {
        public static IActionResponse<FileExportResultModel> DownloadExcelFile(EntityName? entityName, string dateTimeFrom, string dateTimeTo)
        {
            if (entityName == null || string.IsNullOrEmpty(dateTimeFrom) || string.IsNullOrEmpty(dateTimeTo)) return new ActionResponse<FileExportResultModel> { Message = LocalMessage.InvalidFormData };

            DateTime dateFrom = PersianDateTime.Parse(dateTimeFrom).ToDateTime();
            DateTime dateTo = PersianDateTime.Parse(dateTimeTo).ToDateTime();

            if (dateTo < dateFrom) return new ActionResponse<FileExportResultModel> { Message = LocalMessage.PleaseEnterACorrectDateTimeRange};

            IExportTableBusiness exportToBusiness = null;

            switch (entityName)
            {
                case EntityName.User:
                    exportToBusiness = DependencyResolver.Ioc.IocInitializer.GetInstance<UserBusiness>();
                    break;
                case EntityName.Order:
                    exportToBusiness = DependencyResolver.Ioc.IocInitializer.GetInstance<OrderBusiness>();
                    break;
                default:
                    return new ActionResponse<FileExportResultModel> { Message = LocalMessage.BadRequest };
            }

            return exportToBusiness.ExportExcelFile(new ExportDataTableModel { DateTimeFrom = dateFrom, DateTimeTo = dateTo });
        }
    }
}
