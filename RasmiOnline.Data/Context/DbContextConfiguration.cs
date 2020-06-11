namespace RasmiOnline.Data
{
    using System.Data.Entity;
    using Gnu.Framework.EntityFramework.DataAccess;

    public class DbContextConfiguration : DbConfiguration
    {
        public DbContextConfiguration()
        {
            AddInterceptor(new YKInterceptor());
            AddInterceptor(new NumericInterceptor());
        }
    }
}
