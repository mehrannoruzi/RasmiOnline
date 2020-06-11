namespace RasmiOnline.Console.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Dto;
    using RasmiOnline.Domain.Enum;
    using RasmiOnline.Domain.Entity;
    using System.Collections.Generic;
    using RasmiOnline.Business.Protocol;
    using RasmiOnline.Console.Properties;
    using RasmiOnline.Business.Implement;
    using Gnu.Framework.Core.Validation;
    using Gnu.Framework.Core.Authentication;

    public partial class DiscountController : Controller
    {
        #region Contructor
        private readonly IDiscountBusiness _discountBusiness;
        private readonly IMessageBusiness _messageBusiness;
        private readonly Lazy<IOrderItemBusiness> _orderItemBusiness;
        public DiscountController(IDiscountBusiness discountBusiness, IMessageBusiness messageBusiness, Lazy<IOrderItemBusiness> orderItemBusiness)
        {
            _discountBusiness = discountBusiness;
            _messageBusiness = messageBusiness;
            _orderItemBusiness = orderItemBusiness;
        }
        #endregion

        #region private methods
        [NonAction]
        private void GetIsOneTime(bool isOneTime = false)
        {
            var list = new List<ItemTextValueModel<string, bool>>();
            list.Add(new ItemTextValueModel<string, bool> { Value = false, Key = LocalMessage.MultipleTime });
            list.Add(new ItemTextValueModel<string, bool> { Value = true, Key = LocalMessage.OneTime });

            ViewBag.UsingType = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = (s.Value == isOneTime)
            }).ToList();
        }

        [NonAction]
        private void GetIsFirstTime(bool isFirstTime = false)
        {
            var list = new List<ItemTextValueModel<string, bool>>();
            list.Add(new ItemTextValueModel<string, bool> { Value = false, Key = LocalMessage.FreeTime });
            list.Add(new ItemTextValueModel<string, bool> { Value = true, Key = LocalMessage.FirstTime });

            ViewBag.AccessType = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = (s.Value == isFirstTime)
            }).ToList();
        }

        private void GetDiscountTypes(string discountType = nameof(LocalMessage.PleaseSelect))
        {
            var list = new List<ItemTextValueModel<string, string>>();
            list.Add(new ItemTextValueModel<string, string> { Key = LocalMessage.PleaseSelect, Value = nameof(LocalMessage.PleaseSelect) });

            var result = EnumConvertor.GetEnumElements<DiscountType>().ToList();
            result.ForEach(item =>
            {
                list.Add(new ItemTextValueModel<string, string>() { Key = item.Description, Value = item.Name });
            });

            ViewBag.DiscountTypes = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = s.Value == discountType
            }).ToList();
        }

        private void GetCodeTypes(string codeType = nameof(LocalMessage.PleaseSelect))
        {
            var list = new List<ItemTextValueModel<string, string>>();
            list.Add(new ItemTextValueModel<string, string> { Key = LocalMessage.PleaseSelect, Value = nameof(LocalMessage.PleaseSelect) });

            var result = EnumConvertor.GetEnumElements<CodeType>().ToList();
            result.ForEach(item =>
            {
                list.Add(new ItemTextValueModel<string, string>() { Key = item.Description, Value = item.Name });
            });

            ViewBag.CodeTypes = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = s.Value == codeType
            }).ToList();
        }
        #endregion

        [HttpGet]
        public virtual ActionResult GenerateCode()
        {
            GetIsOneTime();
            GetIsFirstTime();
            GetDiscountTypes();
            //GetCodeTypes();
            return View(new AddDiscountModel
            {
                Ceiling = 0,
                Count = 1,
                CodeLength = 12,
                Value = 0,
                ValidFromDateSh = PersianDateTime.Now.ToString(PersianDateTimeFormat.Date),
                ValidToDateSh = PersianDateTime.Now.AddDays(10).ToString(PersianDateTimeFormat.Date),
                CodeType = CodeType.Both,
                Code = DiscountBusiness.GenerateCode()
            });
        }

        public virtual JsonResult SubmitChange(AddDiscountModel model)
        {
            if (model.DiscountType == 0)
                return Json(new ActionResponse<bool> { Message = LocalMessage.InvalidDiscountType }, JsonRequestBehavior.AllowGet);

            if (model.CodeType == 0)
                return Json(new ActionResponse<bool> { Message = LocalMessage.InvalidCodeType }, JsonRequestBehavior.AllowGet);

            if (model.Value == 0)
                return Json(new ActionResponse<bool> { Message = LocalMessage.InvalidValue }, JsonRequestBehavior.AllowGet);

            if (model.Ceiling == 0 && model.DiscountType == DiscountType.PercentageWithCeiling)
                return Json(new ActionResponse<bool> { Message = LocalMessage.InvalidCelling }, JsonRequestBehavior.AllowGet);

            if (model.IsNotNull().And(ModelState.IsValid))
            {
                model.UserId = (User as ICurrentUserPrincipal).UserId;
                return Json(_discountBusiness.Generate(model), JsonRequestBehavior.AllowGet);
            }

            return Json(new ActionResponse<bool> { Message = LocalMessage.BadRequest }, JsonRequestBehavior.AllowGet);
        }

        [AllowAnonymous]
        public virtual PartialViewResult GetList() => PartialView(MVC.Discount.Views.Partials._List, _discountBusiness.GetList());

        [HttpPost]
        public virtual JsonResult SendToMobile(int id, long mobile)
        {
            var response = new ActionResponse<bool>();
            if (!CellPhoneNumberValidator.IsCellPhoneNumber(mobile.ToString()))
                return Json(new ActionResponse<bool> { Message = LocalMessage.InvalidMobileNumber }, JsonRequestBehavior.AllowGet);

            var findedCode = _discountBusiness.Get(id);
            if (!findedCode.IsSuccessful)
                return Json(new ActionResponse<bool> { Message = findedCode.Message }, JsonRequestBehavior.AllowGet);

            var addToMessage = _messageBusiness.Insert(new Message { Receiver = mobile.ToString(), Type = MessagingType.Sms, Content = string.Format(LocalMessage.SendMessageDescription, findedCode.Result.Code) });
            if (!addToMessage.IsSuccessful)
                return Json(new ActionResponse<bool> { Message = addToMessage.Message }, JsonRequestBehavior.AllowGet);
            else
                return Json(new ActionResponse<bool> { IsSuccessful = true, Message = addToMessage.Message }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public virtual JsonResult Check(DiscountCheckModel model)
        {
            model.TotalPrice = _orderItemBusiness.Value.GetTotalPrice(model.OrderId, model.LangType, true);
            model.UserId = model.UserId;
            return Json(_discountBusiness.Check(model));
        }
            
    }
}