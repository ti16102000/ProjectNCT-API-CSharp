using NganLuongGateway.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC
{
    public class NLService
    {
        public object CreateOrdByNL(decimal money)
        {
            var data = new NLModel()
            {
                return_url = "http://localhost:63252/NganLuong/NLCallBack",
                receiver = "ti16102000@gmail.com",
                transaction_info = "Thanh toan bang NL",
                order_code = Convert.ToString((new Random()).Next(100000)),
                price = money.ToString(),
                currency = "vnd",
                quantity = "1",
                tax = "0",
                discount = "0",
                fee_cal = "0",
                fee_shipping = "0",
                order_description = "Thanh toan goi vip nghe nhac",
                buyer_info = "",
                affiliate_code = "",
                cancel_url = "http://localhost:63252/Client/BuyVip",
                time_limit = DateTime.Now.AddMinutes(5).ToString("dd/MM/yyyy,hh:mm")
            };
            return new PaymentRequest().buildCheckoutUrlNew(data);
        }
    }
}