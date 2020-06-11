namespace RasmiOnline.Dashboard.Controllers
{
    using Business.Protocol;
    using Gnu.Framework.Core;
    using Gnu.Framework.Core.Authentication;
    using RasmiOnline.Console.Properties;
    using RasmiOnline.Domain.Dto;
    using RasmiOnline.Domain.Entity;
    using RasmiOnline.Domain.Enum;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public partial class BankCardController : Controller
    {

        #region Contructor
        private readonly IBankCardBusiness _BankCardBusiness;
        public BankCardController(IBankCardBusiness BankCardBusiness)
        {
            _BankCardBusiness = BankCardBusiness;
        }
        #endregion

        #region private methods

        private void GetIsActive(bool isActive = true)
        {
            var list = new List<ItemTextValueModel<string, bool?>>();

            list.Add(new ItemTextValueModel<string, bool?> { Value = true, Key = LocalMessage.Yes });
            list.Add(new ItemTextValueModel<string, bool?> { Value = false, Key = LocalMessage.No });

            ViewBag.ActivateStatus = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = (s.Value == isActive)
            }).ToList();
        }

        [NonAction]
        private void GetBankName(string bankName = "PlesaseSelect")
        {
            var list = new List<ItemTextValueModel<string, string>>();

            var result = EnumConvertor.GetEnumElements<BankNames>().ToList();
            result.ForEach(item =>
            {
                list.Add(new ItemTextValueModel<string, string>() { Key = item.Description, Value = item.Name });
            });

            ViewBag.BankNames = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = s.Value == bankName
            }).ToList();
        }


        #endregion

        public virtual ActionResult Manage() => View();

        [AllowAnonymous]
        public virtual PartialViewResult ShowList() => PartialView(MVC.BankCard.Views.Partials._List, _BankCardBusiness.GetAll());


        [HttpGet]
        public virtual PartialViewResult GetForm(int id = default(int))
        {
            GetIsActive();
            if (id > 0)
            {
                var findedBankCard = _BankCardBusiness.Find(id);
                GetBankName(findedBankCard.Result.BankName.ToString());
                return PartialView(MVC.BankCard.Views.Partials._Form, findedBankCard);
            }
            GetBankName();
            return PartialView(MVC.BankCard.Views.Partials._Form, new BankCard());
        }

        [HttpPost]
        public virtual JsonResult FormSubmited(BankCard model)
        {
            var response = new ActionResponse<bool>();
            model.UserId = (User as ICurrentUserPrincipal).UserId;
            if (model.IsNull() || !ModelState.IsValid)
            {
                response.Message = LocalMessage.InvalidFormData;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            IActionResponse<int> bizResult = model.BankCardId <= 0 ? bizResult = _BankCardBusiness.Insert(model) : bizResult = _BankCardBusiness.Update(model);
            response.Result = response.IsSuccessful = bizResult.IsSuccessful;
            response.Message = bizResult.Message;
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}