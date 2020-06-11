namespace RasmiOnline.Domain.Dto
{
    using System;
    using System.Collections.Generic;
    using Gnu.Framework.Core.Authentication;

    public class MenuModel
    {
        public string Menu { get; set; }
        public Guid UserId { get; set; }    
        public List<MenuSPModel> SpResult { get; set; }
        public CustomUserAction DefaultUserAction { get; set; }
        public IEnumerable<UserAction> Items { get; set; }
    }
}
