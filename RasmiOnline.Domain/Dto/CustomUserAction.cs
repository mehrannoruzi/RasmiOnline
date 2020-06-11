namespace RasmiOnline.Domain.Dto
{
    using Gnu.Framework.Core.Authentication;

 public   class CustomUserAction : UserAction
    {
        public int RoleId { get; set; }
    }
}
