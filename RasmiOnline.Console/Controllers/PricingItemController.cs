namespace RasmiOnline.Console.Controllers
{
    using Domain.Dto;
    using Properties;
    using System.Linq;
    using Domain.Enum;
    using System.Web.Mvc;
    using Business.Protocol;
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Entity;
    using System.Collections.Generic;

    public partial class PricingItemController : Controller
    {
        #region Contructor
        private readonly IPricingItemBusiness _pricingItemBusiness;
        public PricingItemController(IPricingItemBusiness pricingItemBusiness)
        {
            _pricingItemBusiness = pricingItemBusiness;
        }
        #endregion

        #region Pirivate Methods
        [NonAction]
        private void GetIsPricingItem(bool? isPricingItem = null, bool showPleaseSelect = true)
        {
            var list = new List<ItemTextValueModel<string, bool?>>();
            if (showPleaseSelect)
                list.Add(new ItemTextValueModel<string, bool?> { Value = null, Key = LocalMessage.PleaseSelect });

            list.Add(new ItemTextValueModel<string, bool?> { Value = true, Key = LocalMessage.Yes });
            list.Add(new ItemTextValueModel<string, bool?> { Value = false, Key = LocalMessage.No });

            ViewBag.IsPricingItems = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = (s.Value == isPricingItem)
            }).ToList();
        }


        [NonAction]
        private void GetIsDiscountable(bool? isDiscountable = null, bool showPleaseSelect = true)
        {
            var list = new List<ItemTextValueModel<string, bool?>>();
            if (showPleaseSelect)
                list.Add(new ItemTextValueModel<string, bool?> { Value = null, Key = LocalMessage.PleaseSelect });

            list.Add(new ItemTextValueModel<string, bool?> { Value = true, Key = LocalMessage.Yes });
            list.Add(new ItemTextValueModel<string, bool?> { Value = false, Key = LocalMessage.No });

            ViewBag.IsDiscountableItems = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = (s.Value == isDiscountable)
            }).ToList();
        }

        [NonAction]
        private void GetIsMustlyUse(bool? isMustlyUse = null, bool showPleaseSelect = true)
        {
            var list = new List<ItemTextValueModel<string, bool?>>();
            if (showPleaseSelect)
                list.Add(new ItemTextValueModel<string, bool?> { Value = null, Key = LocalMessage.PleaseSelect });

            list.Add(new ItemTextValueModel<string, bool?> { Value = true, Key = LocalMessage.Yes });
            list.Add(new ItemTextValueModel<string, bool?> { Value = false, Key = LocalMessage.No });

            ViewBag.IsMustlyUseItems = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = (s.Value == isMustlyUse)
            }).ToList();
        }

        [NonAction]
        private void GetPricingItemCategories(string pricingItemCategory = "0", bool forSearch = false)
        {
            var list = new List<ItemTextValueModel<string, string>>();
            list.Add(new ItemTextValueModel<string, string> { Key = (forSearch ? LocalMessage.All : LocalMessage.PleaseSelect), Value = "0" });

            var result = EnumConvertor.GetEnumElements<PricingItemCategory>().ToList();
            result.ForEach(item =>
            {
                list.Add(new ItemTextValueModel<string, string>() { Key = item.Description, Value = item.Name });
            });

            ViewBag.PricingItemCategories = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = s.Value == pricingItemCategory
            }).ToList();
        }


        [NonAction]
        private void GetPricingItemUnits(string pricingItemUnit = "0", bool forSearch = false)
        {
            var list = new List<ItemTextValueModel<string, string>>();
            list.Add(new ItemTextValueModel<string, string> { Key = (forSearch ? LocalMessage.All : LocalMessage.PleaseSelect), Value = "0" });
            var result = EnumConvertor.GetEnumElements<PricingItemUnit>().ToList();
            result.ForEach(item =>
            {
                list.Add(new ItemTextValueModel<string, string>() { Key = item.Description, Value = item.Name });
            });

            ViewBag.PricingItemUnits = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = s.Value == pricingItemUnit
            }).ToList();
        }

        #endregion

        [HttpGet, AllowAnonymous]
        public virtual JsonResult Get(string str = "", bool? isPricingItem = null) => Json(_pricingItemBusiness.Get(str, isPricingItem), JsonRequestBehavior.AllowGet);

        [HttpGet]
        public virtual ActionResult Search(FilterPricingItemModel model)
        {
            GetPricingItemCategories(model.CategoryId.ToString(), true);
            GetPricingItemUnits(model.PricingItemUnit.ToString(), true);
            var result = _pricingItemBusiness.Search(model);
            if (!Request.IsAjaxRequest()) return View(result);
            return PartialView(MVC.PricingItem.Views.Partials._SearchList, result.Result);
        }

        [HttpGet]
        public virtual PartialViewResult GetForm(int id = default(int))
        {
            if (id > 0)
            {
                var findedPricingItem = _pricingItemBusiness.GetPricingItem(id);
                GetIsPricingItem(findedPricingItem.Result.IsPricingItem, false);
                GetIsMustlyUse(findedPricingItem.Result.IsMustlyUse, false);
                GetIsDiscountable(findedPricingItem.Result.IsDiscountable, false);

                GetPricingItemCategories(findedPricingItem.Result.CategoryId.ToString());
                GetPricingItemUnits(findedPricingItem.Result.PricingItemUnit.ToString());
                return PartialView(MVC.PricingItem.Views.Partials._Form, findedPricingItem);
            }

            GetIsPricingItem(showPleaseSelect: false);
            GetIsMustlyUse(showPleaseSelect: false);
            GetIsDiscountable(showPleaseSelect: false);
            GetPricingItemCategories();
            GetPricingItemUnits();
            return PartialView(MVC.PricingItem.Views.Partials._Form, new ActionResponse<PricingItem> { IsSuccessful = true, Result = new PricingItem { Price = 0 } });
        }

        [HttpPost]
        public virtual JsonResult FormSubmited(PricingItem model)
        {
            var response = new ActionResponse<bool>();

            if (model.CategoryId == 0)
            {
                response.Message = string.Format(LocalMessage.NotSelectedWithFormat, LocalMessage.Category);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            if (model.PricingItemUnit == 0)
            {
                response.Message = string.Format(LocalMessage.NotSelectedWithFormat, LocalMessage.Unit);
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            if (model.IsNull() || !ModelState.IsValid)
            {
                response.Message = LocalMessage.InvalidFormData;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            IActionResponse<int> bizResult = model.PricingItemId <= 0 ? bizResult = _pricingItemBusiness.Insert(model) : bizResult = _pricingItemBusiness.Update(model);
            response.Result = response.IsSuccessful = bizResult.IsSuccessful;
            response.Message = bizResult.Message;
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult Delete(int id = default(int))
            => Json((id > 0 ? _pricingItemBusiness.Delete(id) : new ActionResponse<bool> { Message = LocalMessage.BadRequest }), JsonRequestBehavior.AllowGet);

        [HttpGet]
        public virtual PartialViewResult GetPricingItemTable(CalculatorTemplate templateId)
        {
            ViewBag.CalculatorTemplate = templateId;
            return PartialView(MVC.PricingItem.Views.Partials._PriceItemTable, _pricingItemBusiness.GetPricingItems(templateId));
        }

        [HttpGet, OutputCache(Duration = 3600)]
        public virtual PartialViewResult GetPricingItemAccordion()
            => PartialView(MVC.PricingItem.Views.Partials._PriceItemAccordion, _pricingItemBusiness.GetPricingItems());

        [ChildActionOnly]
        public virtual PartialViewResult MustlyUsed() => PartialView(MVC.PricingItem.Views.Partials._MustlyUsed, _pricingItemBusiness.GetMustlyUsed());
    }
}