namespace RasmiOnline.Business.Implement
{
    using System;
    using Domain.Dto;
    using System.Text;
    using System.Linq;
    using Domain.Enum;
    using Domain.Entity;
    using Business.Protocol;
    using Gnu.Framework.Core;
    using System.Data.Entity;
    using Business.Properties;
    using Gnu.Framework.Core.Log;
    using System.Collections.Generic;
    using System.Security.Cryptography;
    using Gnu.Framework.EntityFramework;
    using Gnu.Framework.EntityFramework.DataAccess;

    public class DiscountBusiness : IDiscountBusiness
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        readonly IDbSet<Discount> _discount;
        readonly IDbSet<Order> _order;
        readonly IDbSet<DiscountOrder> _discountOrder;

        public DiscountBusiness(IUnitOfWork uow, Lazy<IOrderItemBusiness> orderItemBusiness)
        {
            _uow = uow;
            _discount = uow.Set<Discount>();
            _order = uow.Set<Order>();
            _discountOrder = uow.Set<DiscountOrder>();
        }
        #endregion

        public IActionResponse<bool> Generate(AddDiscountModel model)
        {
            var response = new ActionResponse<bool>();

            if (model.Count <= 0)
            {
                response.Message = BusinessMessage.InvalidCodeCount;
                return response;
            }

            if (model.CodeLength < 12)
            {
                response.Message = BusinessMessage.TheMinimumNumberOfCharactersIs8;
                return response;
            }

            int insertedCount = 0;
            while (insertedCount < model.Count)
            {
                using (var dbContextTransaction = _uow.Database.BeginTransaction())
                {
                    try
                    {
                        int tempCount = model.Count - insertedCount;
                        for (int i = 1; i <= (tempCount >= 50 ? 50 : tempCount); i++)
                        {
                            _discount.Add(new Discount
                            {
                                Ceiling = model.Ceiling,
                                Code = model.Code,//GenerateCode(type: model.CodeType),
                                CodeType = model.CodeType,
                                DiscountType = model.DiscountType,
                                ForFirstUser = model.ForFirstUser,
                                UseForOnce = model.UseForOnce,
                                IsUsed = false,
                                ValidFromDateMi = PersianDateTime.Parse(model.ValidFromDateSh).ToDateTime(),
                                ValidFromDateSh = model.ValidFromDateSh,
                                ValidToDateMi = PersianDateTime.Parse(model.ValidToDateSh).ToDateTime(),
                                ValidToDateSh = model.ValidToDateSh,
                                Value = model.Value,
                                UserId = model.UserId,
                            });
                        }

                        var result = _uow.SaveChanges();
                        if (result <= 0) throw new Exception("Unchange or Error in Operation!");
                        dbContextTransaction.Commit();
                        insertedCount += (tempCount >= 50 ? 50 : tempCount);
                    }
                    catch (Exception e)
                    {
                        dbContextTransaction.Rollback();
                        FileLoger.Error(e);
                    }
                }
            }

            response.IsSuccessful = response.Result = (insertedCount == model.Count);
            response.Message = response.IsSuccessful ? BusinessMessage.Success : BusinessMessage.Error;
            return response;
        }

        public IActionResponse<Discount> Get(string code)
        {
            var response = new ActionResponse<Discount>();

            if (!string.IsNullOrEmpty(code))
            {
                response.Message = BusinessMessage.InvalidDiscountCode;
                return response;
            }

            var findedDiscount = _discount.FirstOrDefault(x => x.Code == code);

            if (findedDiscount.IsNull())
                response.Message = BusinessMessage.RecordNotFound;
            else
            {
                response.IsSuccessful = true;
                response.Result = findedDiscount;
            }

            return response;
        }

        public IActionResponse<Discount> Get(int id)
        {
            var response = new ActionResponse<Discount>();

            var findedDiscount = _discount.FirstOrDefault(x => x.DiscountId == id);

            if (findedDiscount.IsNull())
                response.Message = BusinessMessage.RecordNotFound;
            else
            {
                response.IsSuccessful = true;
                response.Result = findedDiscount;
            }

            return response;
        }

        public IActionResponse<bool> Use(string code)
        {
            var response = new ActionResponse<bool>();

            var discount = Get(code);
            if (!discount.IsSuccessful)
                response.Message = discount.Message;
            else
            {
                discount.Result.IsUsed = true;

                _uow.Entry(discount.Result).State = EntityState.Modified;
                var result = _uow.SaveChanges();
                response.IsSuccessful = response.Result = result.ToSaveChangeResult();
                response.Message = result.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error);
            }

            return response;
        }

        public IActionResponse<List<Discount>> GetList(int count = 100)
        {
            var response = new ActionResponse<List<Discount>>();

            var items = _discount.OrderByDescending(x => x.DiscountId).Take(count).AsNoTracking().ToList();

            if (items.IsNotNull().And(items.Any()))
            {
                response.Result = items;
                response.IsSuccessful = true;
            }
            else
                response.Message = BusinessMessage.RecordNotFound;

            return response;
        }

        /// <summary>
        /// Return a unique code
        /// </summary>
        /// <param name="fixedLen">unique code length - default value is 12</param>
        /// <param name="type">code type - default value is Both</param>
        /// <returns></returns>
        public static string GenerateCode(int fixedLen = 12, CodeType type = CodeType.Both)
        {
            char[] upperChars = ("ABCDEFGHIJKLMNOPQRSTUVWXYZ").ToCharArray();
            char[] digit = ("1234567890").ToCharArray();
            char[] bothUpperCharsDigit = ("ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890").ToCharArray();

            byte[] data = new byte[1];
            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetNonZeroBytes(data);
                data = new byte[fixedLen];
                crypto.GetNonZeroBytes(data);
            }

            StringBuilder result = new StringBuilder(fixedLen);
            foreach (byte b in data)
            {
                switch (type)
                {
                    case CodeType.Digit:
                        result.Append(digit[b % (digit.Length)]);
                        break;
                    case CodeType.Character:
                        result.Append(upperChars[b % (upperChars.Length)]);
                        break;
                    default:
                        result.Append(bothUpperCharsDigit[b % (bothUpperCharsDigit.Length)]);
                        break;
                }
            }
            return result.ToString();
        }


        /// <summary>
        /// check discount code 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="orderId"></param>
        /// <returns>Returns Discount</returns>
        public IActionResponse<Discount> Check(DiscountCheckModel model)
        {
            var item = _discount.FirstOrDefault(x => x.Code == model.Code);
            if (item == null) return new ActionResponse<Discount>
            {
                IsSuccessful = false,
                Message = BusinessMessage.InvalidDiscountCode
            };

            if (item.IsUsed) return new ActionResponse<Discount>
            {
                IsSuccessful = false,
                Message = BusinessMessage.DiscountBeenUsed
            };

            if (DateTime.Now < item.ValidFromDateMi) return new ActionResponse<Discount>
            {
                IsSuccessful = false,
                Message = BusinessMessage.DiscountUseDateNotArrived
            };

            if (DateTime.Now > item.ValidToDateMi) return new ActionResponse<Discount>
            {
                IsSuccessful = false,
                Message = BusinessMessage.DiscountBeenExpired
            };
            if (item.UseForOnce && _discountOrder.Any(x => x.DiscountId == item.DiscountId)) return new ActionResponse<Discount>
            {
                IsSuccessful = false,
                Message = BusinessMessage.DiscountBeenUsed
            };

            if (_discountOrder.Any(x => x.OrderId == model.OrderId)) return new ActionResponse<Discount>
            {
                IsSuccessful = false,
                Message = BusinessMessage.OneDiscountCanBeUsedPerOrder
            };

            if (item.ForFirstUser && _discountOrder.Any(x => x.DiscountId == item.DiscountId && x.Order.UserId != model.UserId)) return new ActionResponse<Discount>
            {
                IsSuccessful = false,
                Message = BusinessMessage.DiscountBeenUsed
            };

            if (item.DiscountType != DiscountType.Value)
            {

                var value = model.TotalPrice * item.Value / 100;
                if (item.DiscountType == DiscountType.PercentageWithCeiling)
                    item.Value = value > item.Ceiling ? item.Ceiling : value;
                else
                    item.Value = value;
            }

            if (item.Value > model.TotalPrice) item.Value = model.TotalPrice;
            return new ActionResponse<Discount>
            {
                IsSuccessful = true,
                Result = item
            };
        }


    }
}