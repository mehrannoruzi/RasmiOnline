namespace RasmiOnline.Business.Implement
{
    using System;
    using Protocol;
    using System.Linq;
    using Domain.Entity;
    using Gnu.Framework.Core;
    using System.Data.Entity;
    using Domain.Dto;
    using Domain.Enum;
    using System.Collections.Generic;
    using Business.Properties;
    using Gnu.Framework.EntityFramework;
    using Gnu.Framework.EntityFramework.DataAccess;
    using SharedPreference;
    using Observers;
    using System.Text.RegularExpressions;
    using Gnu.Framework.Core.Security;

    public class OrderBusiness : IOrderBusiness, IExportTableBusiness
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        readonly IDbSet<Order> _order;
        readonly IDbSet<OrderItem> _orderItem;
        readonly IDbSet<DiscountOrder> _discountOrder;
        readonly Lazy<ISettingBusiness> _settingBusiness;
        readonly Lazy<IDiscountBusiness> _discountBusiness;
        readonly Lazy<IPricingItemBusiness> _pricingItemBusiness;
        readonly Lazy<IOrderItemBusiness> _orderItemBusiness;
        readonly Lazy<IObserverManager> _observerManager;
        readonly Lazy<IUserBusiness> _userBusiness;
        public OrderBusiness(IUnitOfWork uow, Lazy<IUserBusiness> userBusiness, Lazy<IObserverManager> observerManager, Lazy<ISettingBusiness> settingBusiness, Lazy<IDiscountBusiness> discountBusiness, Lazy<IPricingItemBusiness> pricingItemBusiness, Lazy<IOrderItemBusiness> orderItemBusiness)
        {
            _uow = uow;
            _order = uow.Set<Order>();
            _orderItem = uow.Set<OrderItem>();
            _discountOrder = uow.Set<DiscountOrder>();
            _discountBusiness = discountBusiness;
            _pricingItemBusiness = pricingItemBusiness;
            _orderItemBusiness = orderItemBusiness;
            _settingBusiness = settingBusiness;
            _observerManager = observerManager;
            _userBusiness = userBusiness;
        }
        #endregion

        /// <summary>
        /// update founded and changed props order
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResponse<Order> Update(Order model)
        {
            _order.Attach(model);
            _uow.Entry(model).State = EntityState.Modified;
            var rep = _uow.SaveChanges();
            if (rep.ToSaveChangeResult())
                StatusNotifier(model);
            return new ActionResponse<Order>
            {
                IsSuccessful = rep.ToSaveChangeResult(),
                Message = rep.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                Result = model
            };
        }


        /// <summary>
        /// update founded and changed props order
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResponse<Order> BriefUpdate(Order model)
        {
            var notifOffice = false;
            var order = _order.Find(model.OrderId);
            if (order == null)
                return new ActionResponse<Order> { IsSuccessful = false, Message = BusinessMessage.RecordNotFound };
            order.OrderStatus = model.OrderStatus;
            order.DayToDelivery = model.DayToDelivery;
            order.DocsBeenRecieved = model.DocsBeenRecieved;
            if (!string.IsNullOrEmpty(model.DeliverFiles_DateSh) && new Regex(RegexPattern.PersianDateTime).IsMatch(model.DeliverFiles_DateSh))
            {
                order.DeliverFiles_DateSh = model.DeliverFiles_DateSh;
                order.DeliverFiles_DateMi = PersianDateTime.Parse(model.DeliverFiles_DateSh).ToDateTime();
            }

            order.OrderDescription = model.OrderDescription;
            order.LangType = model.LangType;

            if (order.OfficeUserId == Guid.Empty && model.OfficeUserId != Guid.Empty)
                notifOffice = true;
            order.OfficeUserId = model.OfficeUserId;

            var rep = _uow.SaveChanges();
            if (rep.ToSaveChangeResult())
                StatusNotifier(order);

            //Notif office once
            if (notifOffice)
            {
                var officeUser = _userBusiness.Value.Find(order.OfficeUserId);
                _observerManager.Value.Notify(ConcreteKey.Order_Moved_Office, new ObserverMessage
                {
                    SmsContent = string.Format(BusinessMessage.Order_Moved_Office, order.OrderId),
                    Key = ConcreteKey.Order_Moved_Office.ToString(),
                    RecordId = order.OrderId,
                    UserId = officeUser.UserId
                });
            }

            return new ActionResponse<Order>
            {
                IsSuccessful = rep.ToSaveChangeResult(),
                Message = rep.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                Result = model
            };
        }

        /// <summary>
        /// this method will be used when order lang or items count changed
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="newLangType"></param>
        /// <param name="items"></param>
        /// <returns></returns>
        public IActionResponse<Order> Update(int orderId, LangType newLangType, IEnumerable<OrderItem> items)
        {
            var orderItem =new OrderItem();
            PricingItem pricingItem;
            var order = Find(orderId, "OrderItems");
            order.LangType = newLangType;
            foreach (var item in items.Where(x => x.OrderItemType == OrderItemType.PricingItem))
            {
                //update
                if (item.OrderItemId != 0)
                {
                    orderItem = order.OrderItems.FirstOrDefault(x => x.OrderItemId == item.OrderItemId);
                    if (orderItem == null) return new ActionResponse<Order> { IsSuccessful = false, Message = BusinessMessage.RecordNotFound };
                    #region update fields
                    if (newLangType == LangType.Fa_En) orderItem.Price = item.Price;
                    else orderItem.Price_OthersLang = item.Price;
                    orderItem.Copy = item.Copy;
                    orderItem.Count = item.Count;
                    #endregion
                    _uow.Entry(orderItem).State = EntityState.Modified;
                }
                else//add
                {
                    pricingItem = _pricingItemBusiness.Value.Find(item.PricingItemId);
                    if (pricingItem == null) return new ActionResponse<Order> { IsSuccessful = false, Message = BusinessMessage.RecordNotFound };
                    #region set fields
                    item.IsPricingItem = pricingItem.IsPricingItem;
                    item.OrderId = orderId;
                    item.Name = pricingItem.DocumentType;
                    item.CopyPrice = pricingItem.CopyPrice;
                    item.CopyPrice_OthersLang = pricingItem.CopyPrice_OthersLang;
                    if (newLangType == LangType.Fa_En) item.Price_OthersLang = pricingItem.Price_OthersLang;
                    else
                    {
                        var temp = item.Price;
                        item.Price = pricingItem.Price_OthersLang;
                        item.Price_OthersLang = temp;
                    }
                    item.OrderItemType = OrderItemType.PricingItem;
                    item.PricingItemUnitText = pricingItem.PricingItemUnitText;
                    #endregion
                    order.OrderItems.Add(item);
                    _uow.Entry(item).State = EntityState.Added;
                }

            }
            #region update official record item 
            orderItem = order.OrderItems.FirstOrDefault(x => x.OrderItemType == OrderItemType.OfficialRecordItem);
            var modelOfficialRecordItem = items.FirstOrDefault(x => x.OrderItemType == OrderItemType.OfficialRecordItem);
            orderItem.Copy = modelOfficialRecordItem.Copy;
            orderItem.Count = modelOfficialRecordItem.Count;
            if (newLangType != LangType.Fa_En) orderItem.Price_OthersLang = modelOfficialRecordItem.Price;
            else orderItem.Price = modelOfficialRecordItem.Price;
            _uow.Entry(orderItem).State = EntityState.Modified;
            #endregion
            _uow.Entry(order).State = EntityState.Modified;
            var rep = _uow.SaveChanges();

            return new ActionResponse<Order>
            {
                IsSuccessful = rep.ToSaveChangeResult(),
                Result = order,
                Message = rep.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error)
            };
        }

        public void StatusNotifier(Order order)
        {
            var user = _userBusiness.Value.Find(order.UserId);

            if (user != null)
            {
                switch (order.OrderStatus)
                {
                    case OrderStatus.WaitForPayment:
                        _observerManager.Value.Notify(ConcreteKey.Waiting_For_Payment, new ObserverMessage
                        {
                            SmsContent = string.Format(BusinessMessage.Waiting_For_Payment, order.OrderId),
                            BotContent = string.Format(BusinessMessage.Change_OrderState_Bot, order.OrderId, order.OrderStatus.GetDescription(), PersianDateTime.Now.ToString(PersianDateTimeFormat.FullDateFullTime)),
                            Key = ConcreteKey.Waiting_For_Payment.ToString(),
                            RecordId = order.OrderId,
                            UserId = user.UserId
                        });
                        break;
                    case OrderStatus.PayAllFactor:
                        _observerManager.Value.Notify(ConcreteKey.Pay_All_Factor, new ObserverMessage
                        {
                            SmsContent = string.Format(BusinessMessage.Pay_All_Factor_Sms, order.OrderId),
                            BotContent = string.Format(BusinessMessage.Change_OrderState_Bot, order.OrderId, order.OrderStatus.GetDescription(), PersianDateTime.Now.ToString(PersianDateTimeFormat.FullDateFullTime)),
                            Key = ConcreteKey.Waiting_For_Payment.ToString(),
                            RecordId = order.OrderId,
                            UserId = user.UserId,
                        });
                        break;
                    case OrderStatus.Cancel:
                        _observerManager.Value.Notify(ConcreteKey.Cancel_Order, new ObserverMessage
                        {
                            BotContent = string.Format(BusinessMessage.Cancel_Order, order.OrderId, PersianDateTime.Now.ToString(PersianDateTimeFormat.FullDateFullTime)),
                            Key = ConcreteKey.Cancel_Order.ToString(),
                            RecordId = order.OrderId,
                            UserId = user.UserId,
                        });
                        break;
                    default:
                        {
                            _observerManager.Value.Notify(ConcreteKey.Change_OrderState, new ObserverMessage
                            {
                                BotContent = string.Format(BusinessMessage.Change_OrderState_Bot, order.OrderId, order.OrderStatus.GetDescription(), PersianDateTime.Now.ToString(PersianDateTimeFormat.FullDateFullTime)),
                                Key = ConcreteKey.Change_OrderState.ToString(),
                                RecordId = order.OrderId,
                                UserId = user.UserId,
                            });
                            break;
                        }

                }

                //insert ShortLink 

            }

        }

        /// <summary>
        /// insert order with it's items(detailed add)
        /// </summary>
        /// <param name="description"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResponse<Order> Insert(Order model)
        {
            #region Init Some of Props
            var _setting = _settingBusiness.Value.Get();
            model.DayToDelivery = _setting.DayToDelivery;
            #endregion
            #region adding order items
            PricingItem pricingItem;
            foreach (var item in model.OrderItems)
            {
                pricingItem = _pricingItemBusiness.Value.Find(item.PricingItemId);
                if (pricingItem == null) return new ActionResponse<Order> { IsSuccessful = false, Message = BusinessMessage.RecordNotFound };
                item.Name = pricingItem.DocumentType;
                item.OrderItemType = OrderItemType.PricingItem;
                item.IsPricingItem = pricingItem.IsPricingItem;
                item.Price = pricingItem.Price;// model.LangType == LangType.Fa_En ? pricingItem.Price : pricingItem.Price_OthersLang;
                item.CopyPrice = pricingItem.CopyPrice;//model.LangType == LangType.Fa_En ? pricingItem.CopyPrice : pricingItem.CopyPrice_OthersLang;
                item.Price_OthersLang = pricingItem.Price_OthersLang;
                item.CopyPrice_OthersLang = pricingItem.CopyPrice_OthersLang;
                item.PricingItemUnitText = pricingItem.PricingItemUnitText;
                item.Description = pricingItem.Description;
            }
            #endregion
            #region adding official record item
            int itemsCount = model.OrderItems.Sum(x => x.Count), copyCount = model.OrderItems.Sum(x => x.Copy);
            var _officialPrice = new OrderItem
            {
                OrderItemType = OrderItemType.OfficialRecordItem,
                Count = itemsCount,
                Copy = copyCount,
                Price = _setting.OfficialRecordPrice,
                Price_OthersLang = _setting.OfficialRecordPrice,
                CopyPrice_OthersLang = 0,
                Name = "هزینه ثبت دفتری"
            };
            model.OrderItems.Add(_officialPrice);
            #endregion
            _order.Add(model);
            var rep = _uow.SaveChanges();
            if (rep.ToSaveChangeResult())
            {
                _observerManager.Value.Notify(ConcreteKey.Order_Add, new ObserverMessage
                {
                    SmsContent = string.Format(BusinessMessage.Order_Add_Sms, model.OrderId),
                    BotContent = string.Format(BusinessMessage.Order_Add_Bot, model.OrderId, model.OrderTitle, model.LangType.GetLocalizeDescription(), PersianDateTime.Now.ToString(PersianDateTimeFormat.FullDateFullTime)),
                    Key = ConcreteKey.Order_Add.ToString(),
                    RecordId = model.OrderId,
                    UserId = model.UserId
                });
            }
            return new ActionResponse<Order>
            {
                IsSuccessful = rep.ToSaveChangeResult(),
                Result = model,
                Message = rep.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error)
            };

        }

        public IActionResponse<int> InsertBehalfOfUser(Order model, int roleId)
        {
            var user = _uow.Set<User>().FirstOrDefault(x => x.Email == model.User.Email);
            if (user == null)
            {
                #region Set New User Ptops
                if(string.IsNullOrWhiteSpace(model.User.FirstName))  model.User.FirstName = BusinessMessage.User;
                if (string.IsNullOrWhiteSpace(model.User.LastName)) model.User.LastName = BusinessMessage.New;
                model.User.RegisterDateMi = model.User.LastLoginDateMi = DateTime.Now;
                model.User.RegisterDateSh = model.User.LastLoginDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date);
                model.User.IsActive = true;
                model.User.UserId = Guid.NewGuid();
                model.User.Password = HashGenerator.Hash(model.User.MobileNumber.ToString().Trim(), HashAlgorithms.SHA1);
                #endregion
            }
            else model.User = user;
            PricingItem pricingItem;
            foreach (var item in model.OrderItems.Where(x => x.OrderItemType == OrderItemType.PricingItem))
            {
                pricingItem = _pricingItemBusiness.Value.Find(item.PricingItemId);
                if (pricingItem == null) return new ActionResponse<int> { IsSuccessful = false, Message = BusinessMessage.RecordNotFound };
                item.Name = pricingItem.DocumentType;
                item.IsPricingItem = item.OrderItemType == OrderItemType.PricingItem;
                if (model.LangType != LangType.Fa_En) item.Price_OthersLang = item.Price;
                else item.Price_OthersLang = pricingItem.CopyPrice_OthersLang;
                item.CopyPrice = pricingItem.CopyPrice;//model.LangType == LangType.Fa_En ? pricingItem.CopyPrice : pricingItem.CopyPrice_OthersLang;
                item.PricingItemUnitText = pricingItem.PricingItemUnitText;
                item.Description = pricingItem.Description;
            }
            var oRItem = model.OrderItems.First(x => x.OrderItemType == OrderItemType.OfficialRecordItem);
            oRItem.Name = BusinessMessage.OfficialCost;
            using (var t = _uow.Database.BeginTransaction())
            {
                model.UserId = model.User.UserId;
                model.OrderStatus = OrderStatus.WaitForPricing;
                _uow.Set<Order>().Add(model);
                if (_uow.SaveChanges() > 0)
                {
                    if (user != null)
                    {
                        t.Commit();
                        return new ActionResponse<int>
                        {
                            IsSuccessful = true,
                            Result = model.OrderId
                        };
                    }
                    _uow.Set<UserInRole>().Add(new UserInRole
                    {
                        IsActive = true,
                        UserId = model.User.UserId,
                        RoleId = roleId,
                        ExpireDateMi = DateTime.Now.AddYears(10),
                        ExpireDateSh = PersianDateTime.Now.AddYears(10).ToString(PersianDateTimeFormat.Date)
                    });
                    if (_uow.SaveChanges() > 0)
                    {
                        t.Commit();
                        //_observerManager.Value.Notify(ConcreteKey.User_Register, new ObserverMessage
                        //{
                        //    BotContent = string.Format(BusinessMessage.User_Register, $"{ model.User.FirstName} { model.User.LastName}", $"{DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}"),
                        //    UserId = model.User.UserId,
                        //    Key = $"{ConcreteKey.User_Register}|{ model.User.UserId}",
                        //});
                        return new ActionResponse<int>
                        {
                            IsSuccessful = true,
                            Result = model.OrderId
                        };
                    }
                    else t.Rollback();

                }
                else
                {
                    t.Rollback();
                }
            }
            return new ActionResponse<int>
            {
                IsSuccessful = false,
                Message = BusinessMessage.Error
            };
        }

        /// <summary>
        /// update order with new status
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="newOrderStatus"></param>
        /// <returns></returns>
        public IActionResponse<Order> UpdateStatus(int orderId)
        {
            var order = _order.Find(orderId);
            if (order == null) return new ActionResponse<Order> { IsSuccessful = false, Message = BusinessMessage.RecordNotFound };
            var newOrderStatus = order.GetNextStatus();
            if ((order.OrderStatus == OrderStatus.WaitForPayment) && order.DeliverFiles_DateMi == null)
            {
                order.DeliverFiles_DateMi = GlobalMethod.GetWorkDate(order.DayToDelivery);
                order.DeliverFiles_DateSh = PersianDateTime.Parse((DateTime)order.DeliverFiles_DateMi).ToString(PersianDateTimeFormat.Date);
            }
            order.OrderStatus = newOrderStatus;
            _uow.Entry(order).State = EntityState.Modified;
            var rep = _uow.SaveChanges();

            return new ActionResponse<Order>
            {
                IsSuccessful = rep.ToSaveChangeResult(),
                Message = rep.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                Result = order
            };
        }

        /// <summary>
        /// update order with new status
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="newOrderStatus"></param>
        /// <returns></returns>
        public IActionResponse<Order> UpdateOrderDeliverFiles(int orderId)
        {
            var order = _order.Find(orderId);
            if (order == null) return new ActionResponse<Order> { IsSuccessful = false, Message = BusinessMessage.RecordNotFound };
            if (order.DeliverFiles_DateMi == null)
            {
                order.DeliverFiles_DateMi = GlobalMethod.GetWorkDate(order.DayToDelivery);
                order.DeliverFiles_DateSh = PersianDateTime.Parse((DateTime)order.DeliverFiles_DateMi).ToString(PersianDateTimeFormat.Date);
            }
            _uow.Entry(order).State = EntityState.Modified;
            var rep = _uow.SaveChanges();

            return new ActionResponse<Order>
            {
                IsSuccessful = rep.ToSaveChangeResult(),
                Message = rep.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                Result = order
            };
        }

        public Order Find(int orderId, string relatedEntities = "none")
        {
            var query = _order.Where(x => x.OrderId == orderId);
            switch (relatedEntities)
            {
                case "none":
                    break;
                case "all":
                    query = query.Include(x => x.User).AsNoTracking();
                    query = query.Include(x => x.OrderItems).AsNoTracking();
                    query = query.Include(x => x.Attachments).AsNoTracking();
                    query = query.Include(x => x.Transactions).AsNoTracking();
                    query = query.Include(x => x.OrderNames).AsNoTracking();
                    query = query.Include(x => x.Address).AsNoTracking();
                    query = query.Include(x => x.OrderComments).AsNoTracking();
                    query = query.Include(x => x.Transactions.Select(p => p.PaymentGateway)).AsNoTracking();
                    break;
                default:
                    relatedEntities.Split(new char[] { '&', ',' }, StringSplitOptions.RemoveEmptyEntries).ForEach((entity) =>
                    {
                        query = query.Include(entity).AsNoTracking();
                    });
                    break;
            }
            return query.FirstOrDefault();

        }
        public Order Find(int orderId, Guid officeUserId, string relatedEntities = "none")
        {
            var query = _order.Where(x => x.OrderId == orderId && x.OfficeUserId == officeUserId);
            switch (relatedEntities)
            {
                case "none":
                    break;
                case "all":
                    query = query.Include(x => x.User).AsNoTracking();
                    query = query.Include(x => x.OrderItems).AsNoTracking();
                    query = query.Include(x => x.Attachments).AsNoTracking();
                    query = query.Include(x => x.Transactions).AsNoTracking();
                    query = query.Include(x => x.OrderNames).AsNoTracking();
                    query = query.Include(x => x.Address).AsNoTracking();
                    query = query.Include(x => x.OrderComments).AsNoTracking();
                    query = query.Include(x => x.Transactions.Select(p => p.PaymentGateway)).AsNoTracking();
                    break;
                default:
                    relatedEntities.Split(new char[] { '&', ',' }, StringSplitOptions.RemoveEmptyEntries).ForEach((entity) =>
                    {
                        query = query.Include(entity).AsNoTracking();
                    });
                    break;
            }
            return query.FirstOrDefault();
        }

        public IActionResponse<List<OrderDetailsModel>> Search(FilterOrderModel filterModel)
        {
            var response = new ActionResponse<List<OrderDetailsModel>>();
            var result = _order.Include(i => i.User)
                               .Where(x =>
                               (!string.IsNullOrEmpty(filterModel.FirstName) ? x.User.FirstName.Contains(filterModel.FirstName) : true) &&
                               (!string.IsNullOrEmpty(filterModel.LastName) ? x.User.LastName.Contains(filterModel.LastName) : true) &&
                               (!string.IsNullOrEmpty(filterModel.Email) ? x.User.Email == filterModel.Email : true) &&
                               (!string.IsNullOrEmpty(filterModel.NationalCode) ? x.User.NationalCode == filterModel.NationalCode : true) &&
                               (filterModel.DeliverType != null ? x.DeliveryType == filterModel.DeliverType : true) &&
                               (filterModel.OrderNumber != null ? x.OrderId == filterModel.OrderNumber : true) &&
                               (filterModel.OrderStatus != null ? x.OrderStatus == filterModel.OrderStatus : true)
                         )
                         .OrderByDescending(x => x.InsertDateMi)
                         .Select(s => new OrderDetailsModel
                         {
                             DeliverType = s.DeliveryType,
                             OrderTitle = s.OrderTitle,
                             InsertDateMi = s.InsertDateMi,
                             OrderStatus = s.OrderStatus,
                             OrderNumber = s.OrderId,
                             InsertDateSh = s.InsertDateSh,
                             MobileNumber = s.User.MobileNumber,
                             OrderId = s.OrderId,
                             UserFullName = s.User.FirstName + " " + s.User.LastName
                         })
                         .Take(filterModel.ItemsCount)
                         .ToList();

            if (result.Any())
                response.IsSuccessful = true;
            else
                response.Message = BusinessMessage.UserNotFound;

            response.Result = result;
            return response;
        }

        public IActionResponse<FileExportResultModel> ExportExcelFile(ExportDataTableModel filterModel)
        {
            var response = new ActionResponse<FileExportResultModel>();
            var result = (from order in _order.AsNoTracking()
                          join address in _uow.Set<Address>() on order.AddressId equals address.AddressId
                          join user in _uow.Set<User>() on order.UserId equals user.UserId
                          where order.InsertDateMi >= filterModel.DateTimeFrom && order.InsertDateMi <= filterModel.DateTimeTo
                          orderby order.InsertDateMi descending
                          select new ExportOrderToExcelModel
                          {
                              DeliverFiles_DateMi = order.DeliverFiles_DateMi.ToString(),
                              DeliverType = order.DeliveryType,
                              OrderTitle = order.OrderTitle,
                              InsertDateMi = order.InsertDateMi.ToString(),
                              InsertDateSh = order.InsertDateSh,
                              OrderNumber = order.OrderId.ToString(),
                              OrderStatus = order.OrderStatus.ToString(),
                              OwnerFullName = user.FirstName + " " + user.LastName,
                              OwnerMobileNumber = user.MobileNumber.ToString(),
                              ReciverFullAddress = address.FullAddress,
                              ReciverFullName = address.FullName,
                              ReciverLocation = address.Location.ToString(),
                              ReciverMobileNumber = address.MobileNumber.ToString(),
                          }).ToList();
            if (result.IsNotNull().And(result.Any()))
            {
                response.IsSuccessful = true;
                response.Result = new FileExportResultModel { FileResult = result, RecordsCount = result.Count };
            }
            else response.Message = BusinessMessage.RecordNotFound;
            return response;
        }

        public IActionResponse<IEnumerable<Order>> GetAllOrder(Guid? userId = null, Guid? officeUserId = null, OrderStatus? orderStatus = null, int count = 50)
        {
            if (OrderStatus.Done == orderStatus)
                count = 500;
            var response = new ActionResponse<IEnumerable<Order>>();
            var result = _order.Include(x => x.User)
                               .Where(x => (userId != null ? x.UserId == userId : true)
                                  && (officeUserId != null ? x.OfficeUserId == officeUserId : true)
                                && (orderStatus != null && userId == null ? x.OrderStatus == orderStatus : true))
                               .OrderByDescending(x => x.OrderId).Take(count).ToList();
            if (officeUserId == null)
                foreach (var o in result)
                {
                    if (o.OfficeUserId != null)
                    {
                        var orderOffice = _uow.Set<OfficeAddress>().FirstOrDefault(x => x.UserId == o.OfficeUserId);
                        o.DeliveryName = orderOffice?.DeliveryName;

                    }
                }
            if (result.IsNotNull().And(result.Any()))
            {
                response.Result = result;
                response.IsSuccessful = true;
            }
            else response.Message = BusinessMessage.RecordNotFound;
            return response;
        }

        public IActionResponse<List<ItemTextValueModel<OrderStatus, int>>> GetOrderDetails()
        {
            var response = new ActionResponse<List<ItemTextValueModel<OrderStatus, int>>>();
            var orders = (from o in _order
                          group 0 by o.OrderStatus into g
                          select new ItemTextValueModel<OrderStatus, int>
                          {
                              Key = g.Key,
                              Value = g.Count()
                          }).ToList();

            response.Result = orders;
            response.IsSuccessful = true;
            response.Message = orders.Count > 0 ? BusinessMessage.Success : BusinessMessage.RecordNotFound;
            return response;
        }

        /// <summary>
        /// complete order with discount & payment
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public IActionResponse<Order> CompleteOrder(CompleteOrderModel model)
        {
            var order = Find(model.OrderId, "OrderItems");
            if (!string.IsNullOrEmpty(model.DiscountCode))
            {
                var chkRep = _discountBusiness.Value.Check(new DiscountCheckModel
                {
                    Code = model.DiscountCode,
                    UserId = model.UserId,
                    TotalPrice = _orderItemBusiness.Value.GetTotalPrice(order.OrderId, order.LangType, true),
                    OrderId = model.OrderId
                });
                if (!chkRep.IsSuccessful)
                    return new ActionResponse<Order>
                    {
                        IsSuccessful = false,
                        Message = chkRep.Message
                    };
                _orderItem.Add(new OrderItem
                {
                    Name = "تخفیف",
                    OrderId = model.OrderId,
                    OrderItemType = OrderItemType.DiscountItem,
                    Price = (chkRep.Result.Value * -1),
                    Price_OthersLang = 0,
                    CopyPrice_OthersLang = 0,
                    Count = 1,
                    Copy = 0
                });
                if (chkRep.Result.UseForOnce || chkRep.Result.ForFirstUser)
                {
                    chkRep.Result.IsUsed = true;
                    _uow.Entry(chkRep.Result).State = EntityState.Modified;
                }
                _discountOrder.Add(new DiscountOrder
                {
                    OrderId = model.OrderId,
                    DiscountId = chkRep.Result.DiscountId
                });
            }

            //checking
            if (model.DeliveryType == DeliveryType.ByPost && model.PaymentType == PaymentType.InPerson)
                return new ActionResponse<Order>
                {
                    IsSuccessful = false,
                    Message = BusinessMessage.Error
                };

            order.DeliveryType = model.DeliveryType;
            order.PaymentType = model.PaymentType;
            order.AddressId = model.AddressId;
            if (model.PaymentType == PaymentType.InPerson)
                order.OrderStatus = order.GetNextStatus();
            _uow.Entry(order).State = EntityState.Modified;

            var rep = _uow.SaveChanges();
            return new ActionResponse<Order>
            {
                IsSuccessful = rep.ToSaveChangeResult(),
                Message = rep.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error),
                Result = order
            };

        }

        public bool HasOrder(Guid userId) => _order.Count(X => X.UserId == userId) > 0;


        public IActionResponse<List<ItemTextValueModel<OrderStatus, int>>> GetOrderDetails(Guid officeUserId)
        {
            var response = new ActionResponse<List<ItemTextValueModel<OrderStatus, int>>>();
            var orders = (from o in _order
                          where o.OfficeUserId == officeUserId
                          group 0 by o.OrderStatus into g
                          select new ItemTextValueModel<OrderStatus, int>
                          {
                              Key = g.Key,
                              Value = g.Count()
                          }).ToList();

            response.Result = orders;
            response.IsSuccessful = true;
            response.Message = orders.Count > 0 ? BusinessMessage.Success : BusinessMessage.RecordNotFound;
            return response;
        }


        public int TodayOrderCount(Guid? officeUserId = null)
        {
            var date = DateTime.Now.Date;
            var tomorrow = DateTime.Now.AddDays(1).Date;
            return _order.Count(X => (officeUserId != null ? X.OfficeUserId == officeUserId : true) &&
                           X.DeliverFiles_DateMi >= date && X.DeliverFiles_DateMi < tomorrow
                           && X.OrderStatus != OrderStatus.Done && X.OrderStatus != OrderStatus.Cancel);

        }

        public IEnumerable<Order> AllTodayOrder(Guid? officeUserId = null)
        {
            var date = DateTime.Now.Date;
            var tomorrow = DateTime.Now.AddDays(1).Date;
            return _order.Include(x => x.User)
                         .Where(X => (officeUserId != null ? X.OfficeUserId == officeUserId : true)
                                     && X.DeliverFiles_DateMi >= date && X.DeliverFiles_DateMi < tomorrow
                                     && X.OrderStatus != OrderStatus.Done && X.OrderStatus != OrderStatus.Cancel)
                         .OrderByDescending(x => x.OrderId).AsNoTracking().ToList();
        }

        public Dictionary<string, string> GetOrderPaymentInfo(int orderId)
        {
            var order = Find(orderId, "OrderItems,Transactions");
            if (order == null)
                return new Dictionary<string, string> { { "TotalPrice", "0" }, { "TotalPayment", "0" }, { "Description", string.Empty } };
            return new Dictionary<string, string> { { "TotalPrice", order.TotalPrice().ToString("N0") }, { "TotalPayment", order.Transactions.Where(X => X.IsSuccess).Sum(X => X.Price).ToString("N0") }, { "Description", order.OrderDescription } };
        }

        public IEnumerable<Order> AllOlderOrder(Guid? officeUserId = default(Guid?))
        {
            var date = DateTime.Now.Date;
            var tomorrow = DateTime.Now.AddDays(1).Date;
            return _order.Include(x => x.User)
                         .Where(X => (officeUserId != null ? X.OfficeUserId == officeUserId : true)
                                     && X.DeliverFiles_DateMi < date
                                     && X.OrderStatus != OrderStatus.Done && X.OrderStatus != OrderStatus.Cancel)
                         .OrderByDescending(x => x.OrderId).AsNoTracking().ToList();
        }

        public int OlderOrderCount(Guid? officeUserId = default(Guid?))
        {
            var date = DateTime.Now.Date;
            var tomorrow = DateTime.Now.AddDays(1).Date;
            return _order.Count(X => (officeUserId != null ? X.OfficeUserId == officeUserId : true) &&
                           X.DeliverFiles_DateMi < date
                           && X.OrderStatus != OrderStatus.Done && X.OrderStatus != OrderStatus.Cancel);
        }

        public IEnumerable<Order> GetReport(Guid? officeUserId = null, string fromDate = null, string toDate = null)
        {
            var q = _order.Include(x => x.OrderItems).AsQueryable();
            if (officeUserId != null) q = q.Where(x => x.OfficeUserId == officeUserId);
            if (!string.IsNullOrWhiteSpace(fromDate)) q = q.Where(x => x.InsertDateSh.CompareTo(fromDate) > 0);
            if (!string.IsNullOrWhiteSpace(fromDate)) q = q.Where(x => x.InsertDateSh.CompareTo(toDate) < 0);
            return q.OrderByDescending(x => x.InsertDateMi).ToList();
        }

    }
}
