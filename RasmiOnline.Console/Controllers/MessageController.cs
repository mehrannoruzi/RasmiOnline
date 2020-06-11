namespace RasmiOnline.Console.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using Gnu.Framework.Core;
    using RasmiOnline.Domain.Dto;
    using RasmiOnline.Domain.Enum;
    using RasmiOnline.Domain.Entity;
    using RasmiOnline.Business.Protocol;
    using System.Collections.Generic;
    using RasmiOnline.Console.Properties;
    using Gnu.Framework.Core.Validation;

    public partial class MessageController : Controller
    {
        #region Contructor
        private readonly IMessageBusiness _messageBusiness;
        public MessageController(IMessageBusiness messageBusiness)
        {
            _messageBusiness = messageBusiness;
        }
        #endregion

        #region Private Method
        private void GetMessageTypes(string messageType = nameof(LocalMessage.PleaseSelect))
        {
            var list = new List<ItemTextValueModel<string, string>>();
            list.Add(new ItemTextValueModel<string, string> { Key = LocalMessage.PleaseSelect, Value = nameof(LocalMessage.PleaseSelect)});

            var result = EnumConvertor.GetEnumElements<MessagingType>().ToList();
            result.ForEach(item =>
            {
                if (item.Name != nameof(MessagingType. RoboTele))
                    list.Add(new ItemTextValueModel<string, string>()
                    {
                        Key = item.Description,
                        Value = item.Name
                    });
            });

            ViewBag.MessageTypes = list.Select(s => new SelectListItem
            {
                Text = s.Key,
                Value = s.Value.ToString(),
                Selected = s.Value == messageType
            }).ToList();
        }
        #endregion

        [HttpGet]
        public virtual ActionResult Send()
        {
            GetMessageTypes();
            return View(new SendMessageModel());
        }

        [HttpPost]
        public virtual JsonResult Submit(SendMessageModel model)
        {
            var response = new ActionResponse<bool>();
            var validMessageType = new[] { MessagingType.Email, MessagingType.Sms };

            if (!ModelState.IsValid) response.Message = LocalMessage.InvalidFormData;
            else if (!validMessageType.Contains(model.MessagingType))
            {
                response.Message = LocalMessage.InvalidEmailAddress;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else if ((model.MessagingType == MessagingType.Sms).And(!CellPhoneNumberValidator.IsCellPhoneNumber(model.Reciver)))
            {
                response.Message = LocalMessage.InvalidMobileNumber;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else if ((model.MessagingType == MessagingType.Email).And(!EmailValidator.IsEmail(model.Reciver)))
            {
                response.Message = LocalMessage.InvalidEmailAddress;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var result = _messageBusiness.Insert(new Message
                {
                    Content = model.Message,
                    Receiver = model.Reciver,
                    Type = model.MessagingType
                });
                response.IsSuccessful = response.Result = result.IsSuccessful;
                response.Message = result.Message;
            }
            return Json(response, JsonRequestBehavior.AllowGet);
        }


        [AllowAnonymous]
        public virtual PartialViewResult GetList() => PartialView(MVC.Message.Views.Partials._List, _messageBusiness.GetAll());
    }
}