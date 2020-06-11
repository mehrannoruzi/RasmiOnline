namespace RasmiOnline.DependencyResolver.Ioc
{
    using System;
    using StructureMap;
    using Gnu.Framework.Core;
    using Gnu.Framework.Messaging.Msmq;
    using Gnu.Framework.Core.Authentication;
    using Gnu.Framework.EntityFramework.DataAccess;
    using RasmiOnline.Data.Context;
    using RasmiOnline.Business.Protocol;
    using RasmiOnline.Business.Implement;
    using Gnu.Framework.AspNet.Cache;
    using Business.Observers;

    public class IocInitializer
    {
        public static IContainer Container;
        public static void Initialize()
        {
            Container = new Container(x =>
            {
                x.For<ICacheProvider>().Use<HttpRuntimeCache>();
                x.For<Lazy<ICacheProvider>>().Use(c => new Lazy<ICacheProvider>(c.GetInstance<ICacheProvider>));
                x.For<IUnitOfWork>().Use(() => new RasmiDbContext());
                x.For<ICurrentUserPrincipal>().Use<CurrentUserPrincipal>();
                x.For<IMessagingQueue>().Use<MSMQ>();
                x.For<IObserverManager>().Use<ObserverManager>();
                x.For<Lazy<IObserverManager>>().Use(c => new Lazy<IObserverManager>(c.GetInstance<IObserverManager>));

                #region Acl
                x.For<IRoleBusiness>().Use<RoleBusiness>();
                x.For<IViewBusiness>().Use<ViewBusiness>();
                x.For<IViewInRoleBusiness>().Use<ViewInRoleBusiness>();
                x.For<IUserInRoleBusiness>().Use<UserInRoleBusiness>();
                #endregion

                #region Base
                x.For<IAddressBusiness>().Use<AddressBusiness>();
                x.For<Lazy<IAddressBusiness>>().Use(c => new Lazy<IAddressBusiness>(c.GetInstance<IAddressBusiness>)); 
                x.For<IBankCardBusiness>().Use<BankCardBusiness>();
                x.For<IChannelBusiness>().Use<ChannelBusiness>();
                x.For<IDiscountBusiness>().Use<DiscountBusiness>();
                x.For<IMessageBusiness>().Use<MessageBusiness>();
                x.For<Lazy<IMessageBusiness>>().Use(c => new Lazy<IMessageBusiness>(c.GetInstance<IMessageBusiness>));
                x.For<IOfficeAddressBusiness>().Use<OfficeAddressBusiness>();
                x.For<IPaymentGatewayBusiness>().Use<PaymentGatewayBusiness>();
                x.For<IPricingItemBusiness>().Use<PricingItemBusiness>();
                x.For<ISettingBusiness>().Use<SettingBusiness>();
                x.For<IUserBusiness>().Use<UserBusiness>();
                x.For<Lazy<IUserBusiness>>().Use(c => new Lazy<IUserBusiness>(c.GetInstance<IUserBusiness>));
                x.For<IVerificationCodeBusiness>().Use<VerificationCodeBusiness>();
                #endregion

                #region Order
                x.For<IAttachmentBusiness>().Use<AttachmentBusiness>();
                x.For<IOrderBusiness>().Use<OrderBusiness>();
                x.For<IOrderCommentBusiness>().Use<OrderCommentBusiness>();
                x.For<IOrderDetailBusiness>().Use<OrderDetailBusiness>();
                x.For<Lazy<IOrderDetailBusiness>>().Use(c => new Lazy<IOrderDetailBusiness>(c.GetInstance<IOrderDetailBusiness>));
                x.For<IOrderItemBusiness>().Use<OrderItemBusiness>();
                x.For<IOrderNameBusiness>().Use<OrderNameBusiness>();
                x.For<IShortLinkBusiness>().Use<ShortLinkBusiness>();
                x.For<Lazy<IShortLinkBusiness>>().Use(c => new Lazy<IShortLinkBusiness>(c.GetInstance<IShortLinkBusiness>));
                x.For<ITransactionBusiness>().Use<TransactionBusiness>();
                x.For<Lazy<ITransactionBusiness>>().Use(c => new Lazy<ITransactionBusiness>(c.GetInstance<ITransactionBusiness>));
                #endregion

            });
        }

        public static object GetInstance(Type pluginType)
        {
            return Container.GetInstance(pluginType);
        }

        public static TPluginType GetInstance<TPluginType>()
        {
            return Container.GetInstance<TPluginType>();
        }
    }
}
