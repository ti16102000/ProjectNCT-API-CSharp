using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.ModelViews
{
    public class PackageVipView
    {
        public int PVipID { get; set; }
        public string PVipName { get; set; }
        public int PVipMonths { get; set; }
        public int PVipPrice { get; set; }
    }
}