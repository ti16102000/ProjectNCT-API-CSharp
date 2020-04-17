using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.ModelViews
{
    public class PaymentView
    {
        public int PaymentID { get; set; }
        public string PaymentName { get; set; }
        //
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public Nullable<int> PVipID { get; set; }
        public Nullable<int> OrdPrice { get; set; }
        public System.DateTime OrdDayCreate { get; set; }
        //
        
    }
}