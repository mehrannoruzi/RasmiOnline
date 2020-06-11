
namespace RasmiOnline.Console.Controllers
{
    using Gnu.Framework.Core;
    using RasmiOnline.Business.Protocol;
    using RasmiOnline.Console.Properties;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    public partial class ReportController : Controller
    {
        readonly IOfficeAddressBusiness _officeAddressBusiness;
        readonly IOrderBusiness _orderBusiness;
        #region Constructor
        public ReportController(IOrderBusiness orderBusiness,
            IOfficeAddressBusiness officeAddressBusiness)
        {
            _officeAddressBusiness = officeAddressBusiness;
            _orderBusiness = orderBusiness;
        }
        #endregion

        #region Private Methods
        private IEnumerable<SelectListItem> GetOfficeList(Guid? officeUserId = null)
        {
            var office = _officeAddressBusiness.GetAll();
            var lstOffice = new List<SelectListItem>();

            lstOffice.Add(new SelectListItem { Value = Guid.Empty.ToString(), Text = LocalMessage.PleaseSelect });
            lstOffice.AddRange(office.Result.Select(x => new SelectListItem
            {
                Text = $"{x.DeliveryName} :: {x.LangType.GetDescription()}",
                Value = x.UserId.ToString(),
                Selected = (officeUserId != null && x.UserId == officeUserId)
            }));
            return lstOffice;

        }
        #endregion

        [HttpGet]
        public virtual ActionResult AdminManage(ReportModel model)
        {
            ViewBag.Offices = GetOfficeList();
            if (!Request.IsAjaxRequest())
            {
                model.FromDate = PersianDateTime.Now.AddDays(-1).ToString(PersianDateTimeFormat.Date);
                model.Orders = _orderBusiness.GetReport(model.OfficeUserId, model.FromDate, model.ToDate);
                return View(model);
            }
            else
            {
                model.Orders = _orderBusiness.GetReport(model.OfficeUserId, model.FromDate, model.ToDate);
                return PartialView(MVC.Report.Views.Partials._Orders, model.Orders);
            }
        }

        [HttpGet]
        public virtual ActionResult OfficeManage()
        {
            return View();
        }
    }
}