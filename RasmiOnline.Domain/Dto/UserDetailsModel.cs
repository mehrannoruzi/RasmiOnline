namespace RasmiOnline.Domain.Dto
{
    using System;
    using RasmiOnline.Domain.Properties;
    using System.ComponentModel.DataAnnotations;
    public class UserDetailsModel
    {
        public Guid UserId { get; set; }

        [Display(Name = nameof(DisplayName.MobileNumber), ResourceType = typeof(DisplayName))]
        public long MobileNumber { get; set; }

        [Display(Name = nameof(DisplayName.IsActive), ResourceType = typeof(DisplayName))]
        public bool IsActive { get; set; }

        [Display(Name = nameof(DisplayName.RegisterDateMi), ResourceType = typeof(DisplayName))]
        public DateTime RegisterDateMi { get; set; }

        [Display(Name = nameof(DisplayName.LastLoginDate), ResourceType = typeof(DisplayName))]
        public DateTime LastLoginDateMi { get; set; }

        [Display(Name = nameof(DisplayName.NationalCode), ResourceType = typeof(DisplayName))]
        public string NationalCode { get; set; }

        [Display(Name = nameof(DisplayName.FullName), ResourceType = typeof(DisplayName))]
        public string FullName { get; set; }

        [Display(Name = nameof(DisplayName.Email), ResourceType = typeof(DisplayName))]
        public string Email { get; set; }
    }
}
