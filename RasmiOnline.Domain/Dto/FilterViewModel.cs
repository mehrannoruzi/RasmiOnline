namespace RasmiOnline.Domain.Dto
{
    using Gnu.Framework.EntityFramework;
    using RasmiOnline.Domain.Properties;
    using System.ComponentModel.DataAnnotations;

    public class FilterViewModel: FilterBaseModel
    {
        [Display(Name = nameof(DisplayName.IsVisible), ResourceType = typeof(DisplayName))]
        public bool? IsVisible { get; set; }

        [Display(Name = nameof(DisplayName.ParentId), ResourceType = typeof(DisplayName))]
        public int? ParentId { get; set; }

        [Display(Name = nameof(DisplayName.Controller), ResourceType = typeof(DisplayName))]
        [StringLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ControllerName { get; set; }

        [Display(Name = nameof(DisplayName.ActionName), ResourceType = typeof(DisplayName))]
        [StringLength(25, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ActionName { get; set; }

        [Display(Name = nameof(DisplayName.ActionNameFa), ResourceType = typeof(DisplayName))]
        [StringLength(35, ErrorMessageResourceName = nameof(ErrorMessage.MaxLength), ErrorMessageResourceType = typeof(ErrorMessage))]
        public string ActionNameFa { get; set; }
    }
}
