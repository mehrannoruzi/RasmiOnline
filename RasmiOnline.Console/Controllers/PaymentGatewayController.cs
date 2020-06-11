namespace RasmiOnline.Console.Controllers
{
    using Domain.Dto;
    using System.Linq;
    using Domain.Entity;
    using System.Web.Mvc;
    using Business.Protocol;
    using Gnu.Framework.Core;
    using System.Collections.Generic;
    using RasmiOnline.Console.Properties;

    public partial class PaymentGatewayController : Controller
    {
        #region Contructor
        readonly IPaymentGatewayBusiness _paymentGatewayBusiness;

        public PaymentGatewayController(IPaymentGatewayBusiness paymentGatewayBusiness)
        {
            _paymentGatewayBusiness = paymentGatewayBusiness;
        }
        #endregion

        #region private methods
        [NonAction] 
        void GetIsActive(bool isActive = false)
        {
            var list = new List<ItemTextValueModel<string, bool>>();
            list.Add(new ItemTextValueModel<string, bool> { Value = false, Key = LocalMessage.No });
            list.Add(new ItemTextValueModel<string, bool> { Value = true, Key = LocalMessage.Yes });

            ViewBag.ActiveStatus = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = (s.Value == isActive)
            }).ToList();
        }
        #endregion


        public virtual ActionResult Manage() => View(_paymentGatewayBusiness.GetAll());


        [HttpGet]
        public virtual PartialViewResult GetForm(int id = default(int))
        {
            if (id > 0)
            {
                var findedPaymentGateway = _paymentGatewayBusiness.GetPaymentGateway(id);
                GetIsActive(findedPaymentGateway.Result.IsActive);
                return PartialView(MVC.PaymentGateway.Views.Partial._Form, findedPaymentGateway);
            }

            GetIsActive();
            return PartialView(MVC.PaymentGateway.Views.Partial._Form, new ActionResponse<PaymentGateway> { IsSuccessful = true, Result = new PaymentGateway() });
        }


        [HttpPost]
        public virtual JsonResult FormSubmited(PaymentGateway model)
        {
            var response = new ActionResponse<bool>();

            if (model.IsNull() || !ModelState.IsValid)
            {
                response.Message = LocalMessage.InvalidFormData;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            IActionResponse<int> bizResult = model.PaymentGatewayId <= 0 ? new ActionResponse<int> { Message = LocalMessage.InvalidFormData } : _paymentGatewayBusiness.Update(model);
            response.Result = response.IsSuccessful = bizResult.IsSuccessful;
            response.Message = bizResult.Message;
            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}