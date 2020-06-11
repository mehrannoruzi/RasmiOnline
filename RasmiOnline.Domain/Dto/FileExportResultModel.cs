namespace RasmiOnline.Domain.Dto
{
    using Gnu.Framework.Core;
    using System;

    [Serializable]
    public class FileExportResultModel
    {
        public object FileResult { get; set; }
        public int RecordsCount { get; set; }
        public string CreatedDateTimeSh => PersianDateTime.Now.ToString(PersianDateTimeFormat.FullDateFullTime);
    }
}