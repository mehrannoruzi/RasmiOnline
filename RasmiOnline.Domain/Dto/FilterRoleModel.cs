namespace RasmiOnline.Domain.Dto
{
    using RasmiOnline.Domain.Properties;
    using System.ComponentModel.DataAnnotations;

    public class FilterRoleModel: FilterBaseModel
    {
        [Display(Name = nameof(DisplayName.IsActive), ResourceType = typeof(DisplayName))]
        public bool? IsActive { get; set; }

        [Display(Name = nameof(DisplayName.IsDefault), ResourceType = typeof(DisplayName))]
        public bool? IsDefault { get; set; }

        [Display(Name = nameof(DisplayName.RoleNameFa), ResourceType = typeof(DisplayName))]
        public string RoleNameFa { get; set; }

        [Display(Name = nameof(DisplayName.RoleNameEn), ResourceType = typeof(DisplayName))]
        public string RoleNameEn { get; set; }
    }
}