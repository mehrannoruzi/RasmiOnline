namespace RasmiOnline.SharedPreference
{
    using System;
    using System.Web;

    public class GlobalMethod
    {
        public static DateTime GetWorkDate(int dayCount)
        {
            if (dayCount <= 0)
                return DateTime.Now;

            for (int i = 1; i <= dayCount; i++)
            {
                var day = DateTime.Now.AddDays(i);
                if (day.DayOfWeek == DayOfWeek.Friday || day.DayOfWeek == DayOfWeek.Thursday)
                    dayCount++;
            }
            return DateTime.Now.AddDays(dayCount);
        }

        public static string GetDefaultImageUrl(string fileAbsoluteName)
        {
            if (MimeMapping.GetMimeMapping(fileAbsoluteName).StartsWith("image/"))
                return fileAbsoluteName;
            switch (System.IO.Path.GetExtension(fileAbsoluteName))
            {
                case ".pdf":
                    return "/Content/Images/Attachments/pdf.png";
                case ".zip":
                    return "/Content/Images/Attachments/archive.png";
                case ".rar":
                    return "/Content/Images/Attachments/rar.png";
                case ".txt":
                case ".doc":
                case ".docx":
                    return "/Content/Images/Attachments/archive.png";
                case ".mp3":
                case ".wav":
                    return "/Content/Images/Attachments/music.png";
                case ".mp4":
                case ".mkv":
                    return "/Content/Images/Attachments/archive.png";
                default:
                    return "/Content/Images/Attachments/unknown.png";

            }
        }
    }
}
