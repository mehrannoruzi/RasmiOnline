namespace RasmiOnline.Business.Implement
{
    using Protocol;
    using Properties;
    using System.Linq;
    using System.Data.Entity;
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Dto;
    using RasmiOnline.Domain.Enum;
    using RasmiOnline.Domain.Entity;
    using System.Collections.Generic;
    using Gnu.Framework.EntityFramework;
    using Gnu.Framework.EntityFramework.DataAccess;
    using System;

    public class PricingItemBusiness : IPricingItemBusiness
    {
        #region Constructor
        readonly IUnitOfWork _uow;
        readonly IDbSet<PricingItem> _pricingItem;

        public PricingItemBusiness(IUnitOfWork uow)
        {
            _uow = uow;
            _pricingItem = uow.Set<PricingItem>();
        }
        #endregion

        public PricingItem Find(int pricingItemId) => _pricingItem.Find(pricingItemId);

        public IEnumerable<PricingItem> Get(bool isPricingItem) => _pricingItem.Where(x => !x.IsDeleted && x.IsPricingItem == isPricingItem).AsNoTracking().ToList();

        /// <summary>
        /// get a random result of search text
        /// </summary>
        /// <param name="str"></param>
        /// <param name="isThemplate"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public IEnumerable<PricingItem> Get(string str, bool? isPricingItem = null, int count = 500)
        {
            var query = _pricingItem.Where(x => x.Lable.Contains(str) && !x.IsDeleted);
            if (isPricingItem == true)
                query = query.Where(x => x.IsPricingItem == isPricingItem);
            return query.AsNoTracking().Take(count).ToList();
        }

        public IActionResponse<int> Insert(PricingItem model)
        {
            _uow.Entry(model).State = EntityState.Added;
            var res = _uow.SaveChanges();
            return new ActionResponse<int>
            {
                IsSuccessful = res.ToSaveChangeResult(),
                Result = model.PricingItemId,
                Message = res.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error)
            };
        }

        public IActionResponse<int> Update(PricingItem model)
        {
            var response = new ActionResponse<int>();

            var findedPricingItem = GetPricingItem(model.PricingItemId);
            if (!findedPricingItem.IsSuccessful)
                response.Message = findedPricingItem.Message;
            else
            {
                findedPricingItem.Result.UpdateWith(new
                {
                    model.AboveReference,
                    model.CategoryId,
                    model.DocumentType,
                    model.PricingItemUnit,
                    model.Lable,
                    model.IsPricingItem,
                    model.Price,
                    model.CopyPrice,
                    model.CopyPrice_OthersLang,
                    model.Price_OthersLang,
                    model.Description,
                    model.IsMustlyUse,
                    model.IsDiscountable
                });

                _uow.Entry(findedPricingItem.Result).State = EntityState.Modified;

                response.Result = _uow.SaveChanges();
                response.IsSuccessful = response.Result.ToSaveChangeResult();
                response.Message = response.Result.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error);
            }
            return response;
        }

        public IActionResponse<bool> Delete(int pricingItemId)
        {
            var response = new ActionResponse<bool>();
            var findedPriceItem = GetPricingItem(pricingItemId);
            if (!findedPriceItem.IsSuccessful)
                response.Message = findedPriceItem.Message;
            else
            {
                _uow.Set<PricingItem>().Remove(findedPriceItem.Result);
                var result = _uow.SaveChanges();
                response.IsSuccessful = response.Result = result.ToSaveChangeResult();
                response.Message = result.ToSaveChangeMessageResult(BusinessMessage.Success, BusinessMessage.Error);
            }
            return response;
        }

        public IActionResponse<PricingItem> GetPricingItem(int pricingItemId)
        {
            var response = new ActionResponse<PricingItem>();
            var result = _pricingItem.Find(pricingItemId);
            if (result.IsNotNull())
            {
                response.IsSuccessful = true;
                response.Result = result;
            }
            else response.Message = BusinessMessage.RecordNotFound;
            return response;
        }

        public IActionResponse<List<PricingItem>> Search(FilterPricingItemModel filterModel)
        {
            var response = new ActionResponse<List<PricingItem>>();


            var result = _pricingItem.Where(x => !x.IsDeleted &&
                               (!string.IsNullOrEmpty(filterModel.DocumentType) ? x.DocumentType.Contains(filterModel.DocumentType) : true) &&
                               (!string.IsNullOrEmpty(filterModel.Lable) ? x.Lable.Contains(filterModel.Lable) : true) &&
                               (filterModel.PricingItemUnit > 0 ? x.PricingItemUnit == filterModel.PricingItemUnit : true) &&
                               (filterModel.CategoryId > 0 ? x.CategoryId == filterModel.CategoryId : true)
                         )
                         .AsNoTracking()
                         .OrderByDescending(x => x.PricingItemId)
                         .Take(filterModel.ItemsCount)
                         .ToList();

            if (result.Any())
                response.IsSuccessful = true;
            else
                response.Message = BusinessMessage.RecordNotFound;

            response.Result = result;
            return response;
        }

        public IActionResponse<IEnumerable<PricingItem>> GetPricingItems(CalculatorTemplate templateId, int take = 0)
        {
            var response = new ActionResponse<IEnumerable<PricingItem>>();
            response.Result = _uow.Set<PricingItemTemplate>().Include(X => X.PricingItem).Where(x => x.CalculatorTemplate == templateId)
                                          .AsNoTracking()
                                          .OrderByDescending(x => x.PricingItemId)
                                           .Select(X => X.PricingItem)
                                          .ToList();
            if (response.Result.Any())
                response.IsSuccessful = true;
            else
                response.Message = BusinessMessage.RecordNotFound;
            return response;
        }

        public IActionResponse<IEnumerable<PricingItem>> GetPricingItems(PricingItemCategory categoryId, int take = 0)
        {
            var response = new ActionResponse<IEnumerable<PricingItem>>();

            response.Result = _pricingItem.Where(x => !x.IsDeleted && x.CategoryId == categoryId)
                                          .AsNoTracking()
                                          .OrderByDescending(x => x.PricingItemId)
                                          .Take(take)
                                          .ToList();
            if (response.Result.Any())
                response.IsSuccessful = true;
            else
                response.Message = BusinessMessage.RecordNotFound;

            return response;
        }



        public IEnumerable<PricingItem> GetMustlyUsed() => _pricingItem.Where(X => X.IsMustlyUse).AsNoTracking().ToList();

        public IActionResponse<IEnumerable<PricingItem>> GetPricingItems()
        {
            var response = new ActionResponse<IEnumerable<PricingItem>>();

            response.Result = _pricingItem.Where(x => !x.IsDeleted && x.IsPricingItem)
                                          .AsNoTracking()
                                          .OrderByDescending(x => x.PricingItemId)
                                          .ToList();
            if (response.Result.Any())
                response.IsSuccessful = true;
            else
                response.Message = BusinessMessage.RecordNotFound;

            return response;
        }
    }
}