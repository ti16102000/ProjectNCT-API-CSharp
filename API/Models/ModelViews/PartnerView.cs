using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.ModelViews
{
    public class PartnerView
    {
        public int ID { get; set; }
        public string PartnerName { get; set; }
        public string PartnerImage { get; set; }
        public string PartnerLink { get; set; }
        public bool PartnerActive { get; set; }
        public System.DateTime PartnerDayCreate { get; set; }
    }
}