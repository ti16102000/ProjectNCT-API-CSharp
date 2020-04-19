using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.ModelViews
{
    public class OrderView
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> PVipID { get; set; }
        public int PaymentID { get; set; }
        public Nullable<int> OrdPrice { get; set; }
        public System.DateTime OrdDayCreate { get; set; }
        public string UserName { get; set; }
        public string PaymentName { get; set; }
        public string PVipName { get; set; }
    }
}