using API.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class NganLuongController : Controller
    {
        // GET: NganLuong
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult NLPayment()
        {
            if (Session["id"] != null)
            {
                HttpResponseMessage resPvip = APIService.client.GetAsync("PVip/" + (int)Session["id"]).Result;
                var modelPVip = resPvip.Content.ReadAsAsync<PackageVipView>().Result;
                var link = new NLService().CreateOrdByNL(modelPVip.PVipPrice);
                return Redirect(link.ToString());
            }
            return RedirectToAction("BuyVip", "Client");
        }
        public ActionResult NLCallBack()
        {
            HttpResponseMessage resPvip = APIService.client.GetAsync("PVip/" + (int)Session["id"]).Result;
            var modelPVip = resPvip.Content.ReadAsAsync<PackageVipView>().Result;
            var ord = new OrderView { OrdPrice = modelPVip.PVipPrice, PVipID = modelPVip.PVipID, UserID = (int)Session["UserID"], PaymentID = 2 };
            var res = APIService.client.PostAsJsonAsync("Order", ord).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Chúc mừng bạn đã mua VIP thành công! Yeh yeh!";
            }
            else
            {
                TempData["error"] = "Mua VIP xảy ra lỗi huhu";
            }
            Session.Remove("id");
            return RedirectToAction("Index", "Client");
        }
    }
}