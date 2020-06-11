namespace RasmiOnline.Console.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Business.Protocol;
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Dto;
    using RasmiOnline.Domain.Entity;
    using System.Collections.Generic;
    using RasmiOnline.Console.Properties;

    public partial class ViewController : Controller
    {
        #region Contructor
        private readonly IViewBusiness _viewBusiness;

        public ViewController(IViewBusiness viewBusiness)
        {
            _viewBusiness = viewBusiness;
        }
        #endregion

        #region Private Methods

        [NonAction]
        private void GetIsVisible(bool? isVisible = null, bool showPleaseSelect = true)
        {
            var list = new List<ItemTextValueModel<string, bool?>>();
            if (showPleaseSelect) list.Add(new ItemTextValueModel<string, bool?> { Value = null, Key = LocalMessage.All });

            list.Add(new ItemTextValueModel<string, bool?> { Value = true, Key = LocalMessage.Yes });
            list.Add(new ItemTextValueModel<string, bool?> { Value = false, Key = LocalMessage.No });

            ViewBag.VisibleStatus = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = (s.Value == isVisible)
            }).ToList();
        }

        [NonAction]
        private void GetOrderPriority(byte? orderPriority = null, bool showPleaseSelect = true)
        {
            var list = new List<ItemTextValueModel<string, byte?>>();
            if (showPleaseSelect) list.Add(new ItemTextValueModel<string, byte?> { Value = 0 , Key = LocalMessage.All });

            for (int i = 1; i <= 250; i++)
                list.Add(new ItemTextValueModel<string, byte?> { Key = i.ToString(), Value = (byte?)i });

            ViewBag.OrderPriorities = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = (s.Value == orderPriority)
            }).ToList();
        }

        [NonAction]
        private void GetParents(int? isParentId = null, bool showPleaseSelect = true)
        {
            var list = new List<ItemTextValueModel<string, int?>>();
            if (showPleaseSelect) list.Add(new ItemTextValueModel<string, int?> { Value = null, Key = LocalMessage.All });
            else list.Add(new ItemTextValueModel<string, int?> { Value = 0, Key = LocalMessage.Parent });

            var result = _viewBusiness.GetParentViews();
            result.ForEach(item => { list.Add(new ItemTextValueModel<string, int?>() { Key = item.Key, Value = item.Value }); });

            ViewBag.AllViews = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = (s.Value == isParentId)
            }).ToList();
        }
        #endregion

        [HttpGet]
        public virtual ActionResult Search(FilterViewModel model)
        {
            GetIsVisible();
            GetParents();
            GetOrderPriority();
            var result = _viewBusiness.Search(model);
            if (!Request.IsAjaxRequest()) return View(result);

            return PartialView(MVC.View.Views.Partials._SearchList, result.Result);
        }

        [HttpGet]
        public virtual PartialViewResult GetForm(int id = default(int))
        {
            if (id > 0)
            {
                var findedView = _viewBusiness.GetView(id);
                GetIsVisible(findedView.Result.IsDefault, false);
                GetParents(findedView.Result.ParentId, false);
                GetOrderPriority(findedView.Result.OrderPriority, false);
                return PartialView(MVC.View.Views.Partials._Form, findedView);
            }

            GetIsVisible(showPleaseSelect: false);
            GetParents(showPleaseSelect: false);
            GetOrderPriority(showPleaseSelect: false);
            return PartialView(MVC.View.Views.Partials._Form, new ActionResponse<View> { IsSuccessful = true, Result = new View { ExpireDateSh = PersianDateTime.Now.AddYears(10).ToString(PersianDateTimeFormat.Date) } });
        }

        [HttpPost]
        public virtual JsonResult FormSubmited(View model)
        {
            var response = new ActionResponse<bool>();

            if (model.ParentId > 0)
            {
                if (string.IsNullOrEmpty(model.Controller))
                {
                    response.Message = LocalMessage.InvalidControllerName;
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
                if (string.IsNullOrEmpty(model.ActionName))
                {
                    response.Message = LocalMessage.InvalidActionName;
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                if (string.IsNullOrEmpty(model.Icon))
                {
                    response.Message = LocalMessage.InvalidIcon;
                    return Json(response, JsonRequestBehavior.AllowGet);
                }
            }

            if (model.IsNull() || !ModelState.IsValid)
            {
                response.Message = LocalMessage.InvalidFormData;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            IActionResponse<int> bizResult = model.ViewId <= 0 ? bizResult = _viewBusiness.Insert(model) : bizResult = _viewBusiness.Update(model);
            response.Result = response.IsSuccessful = bizResult.IsSuccessful;
            response.Message = bizResult.Message;
            return Json(response, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public virtual JsonResult Delete(int id = default(int))
            => Json((id > 0 ? _viewBusiness.Delete(id) : new ActionResponse<bool> { Message = LocalMessage.BadRequest }), JsonRequestBehavior.AllowGet);
    }
}