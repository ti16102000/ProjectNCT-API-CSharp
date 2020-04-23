using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NganLuongGateway.Model
{
    public class NLModel
    {
        public string return_url { get; set; }
        public string receiver { get; set; }
        public string transaction_info { get; set; }
        public string order_code { get; set; }
        public string price { get; set; }
        public string currency { get; set; }
        public string quantity { get; set; }
        public string tax { get; set; }
        public string discount { get; set; }
        public string fee_cal { get; set; }
        public string fee_shipping { get; set; }
        public string order_description { get; set; }
        public string buyer_info { get; set; }
        public string affiliate_code { get; set; }
        public string cancel_url { get; set; }
        public string time_limit { get; set; }
    }
}