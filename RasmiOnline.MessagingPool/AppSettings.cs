 namespace RasmiOnline.MessagingPool
{
    using System.Configuration;
    public static class AppSettings {
        public static string TelegramToken => ConfigurationManager.AppSettings["TelegramToken"];
    }
}