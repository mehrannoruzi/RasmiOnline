namespace RasmiOnline.Domain.Dto
{
    using RasmiOnline.Domain.Enum;
    using RasmiOnline.Domain.Properties;
    using System.ComponentModel.DataAnnotations;

    public class SendMessageModel
    {
        [Display(Name = nameof(DisplayName.Sender), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Reciver { get; set; }


        [Display(Name = nameof(DisplayName.MessageType), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public MessagingType MessagingType { get; set; }


        [Display(Name = nameof(DisplayName.Message), ResourceType = typeof(DisplayName))]
        [Required(ErrorMessageResourceName = nameof(ErrorMessage.Required), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string Message { get; set; }
    }
}