namespace RasmiOnline.Business.Protocol
{
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Dto;

    public interface IExportTableBusiness
    {
        IActionResponse<FileExportResultModel> ExportExcelFile(ExportDataTableModel filterModel);
    }
}