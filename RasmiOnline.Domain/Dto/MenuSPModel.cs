namespace RasmiOnline.Domain.Dto
{
    using Entity;
    using System.Linq;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;
    using System.ComponentModel.DataAnnotations.Schema;

    [NotMapped]
    public class MenuSPModel : View
    {
        public string Views { get; set; }

        
        public bool IsView { get; set; }

        [NotMapped]
        public int SubViewsCount { get { return ViewsList.Count(); } }

        [NotMapped]
        public bool HasView => !string.IsNullOrEmpty(Views);

        [NotMapped]
        public List<View> ViewsList => HasView ? new JavaScriptSerializer().Deserialize<List<View>>(Views) : new List<View>();
    }
}
