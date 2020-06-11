namespace RasmiOnline.Console
{
    using RasmiOnline.Domain.Entity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public class ReportModel
    {

        public string FromDate { get; set; }

        public string ToDate { get; set; }

        public Guid? OfficeUserId { get; set; }

        public IEnumerable<Order> Orders { get; set; }
    }
}