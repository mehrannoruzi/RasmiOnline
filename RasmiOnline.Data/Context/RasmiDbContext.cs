namespace RasmiOnline.Data.Context
{
    using System;
    using Domain.Entity;
    using Gnu.Framework.Core;
    using System.Data.Entity;
    using Gnu.Framework.Core.Log;
    using RasmiOnline.SharedPreference;
    using System.Data.Entity.Validation;
    using System.Data.Entity.Infrastructure;
    using Gnu.Framework.EntityFramework.DataAccess;

    public class RasmiDbContext : DbContext, IUnitOfWork
    {
        #region Acl
        public DbSet<UserInRole> UserInRole { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<ViewInRole> ViewInRole { get; set; }
        public DbSet<View> View { get; set; }
        #endregion

        public DbSet<User> User { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<Setting> Setting { get; set; }
        public DbSet<Channel> Channel { get; set; }
        public DbSet<OfficeAddress> OfficeAddress { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<OrderComment> OrderComment { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<PaymentGateway> PaymentGateway { get; set; }
        public DbSet<ActivityLog> ActivityLog { get; set; }
        public DbSet<Attachment> Attachment { get; set; }
        public DbSet<OrderName> OrderName { get; set; }
        public DbSet<Discount> Discount { get; set; }
        public DbSet<PricingItem> PricingItem { get; set; }
        public DbSet<VerificationCode> VerificationCode { get; set; }
        public DbSet<DiscountOrder> DiscountOrder { get; set; }
        public DbSet<ShortLink> ShortLink { get; set; }
        public DbSet<BankCard> BankCard { get; set; }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public override int SaveChanges()
        {
            try
            {
                //try { AuditLoger.Log(this, (HttpContext.Current.User as ICurrentUserPrincipal).UserId); } catch { }
                BasePropertyInitializer.Initialize(this);
                return base.SaveChanges();
            }
            catch (DbEntityValidationException validationException)
            {
                string errorMessage = string.Empty;
                foreach (var errors in validationException.EntityValidationErrors)
                {
                    errorMessage += $"Entity of type \"{errors.Entry.Entity.GetType().Name}\" in state \"{errors.Entry.State.ToString()}\" has the following validation errors:" + Environment.NewLine;
                    foreach (var error in errors.ValidationErrors)
                    {
                        errorMessage += $"- Property: \"{error.PropertyName}\", Error: \"{error.ErrorMessage}\"";
                    }
                }
                errorMessage += Environment.NewLine + Environment.NewLine;
                FileLoger.Error(new Exception(errorMessage), GlobalVariable.LogPath);
                return -3;
            }
            catch (DbUpdateConcurrencyException concurrencyException)
            {
                FileLoger.Error(concurrencyException, GlobalVariable.LogPath);
                return -2;
            }
            catch (DbUpdateException updateException)
            {
                FileLoger.Error(updateException, GlobalVariable.LogPath);
                if (updateException.InnerException.IsNotNull() &&
                    updateException.InnerException.InnerException.IsNotNull() &&
                    updateException.InnerException.InnerException.Message.Contains("Cannot insert duplicate key")) return -4;
                return -1;
            }
            catch (Exception e)
            {
                FileLoger.Error(e, GlobalVariable.LogPath);
                if (e.Message.ToLower().Contains("Cannot insert duplicate key")) return -4;
                return -10;
            }
        }
    }
}
