using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using API.Models.ModelViews;

namespace MVC.Controllers
{
    public class MomoController : Controller
    {
        // GET: Momo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MomoWithPayment()
        {
            if (Session["id"] != null)
            {
                HttpResponseMessage resPvip = APIService.client.GetAsync("PVip/" + (int)Session["id"]).Result;
                var modelPVip = resPvip.Content.ReadAsAsync<PackageVipView>().Result;
                var link = new MomoService().CreateOrderByMomo(Guid.NewGuid(), modelPVip.PVipPrice);
                return Redirect(link.ToString());
            }
            return RedirectToAction("BuyVip", "Client");
        }
        public ActionResult MomoCallBack()
        {
            HttpResponseMessage resPvip = APIService.client.GetAsync("PVip/" + (int)Session["id"]).Result;
            var modelPVip = resPvip.Content.ReadAsAsync<PackageVipView>().Result;
            var ord = new OrderView { OrdPrice = modelPVip.PVipPrice, PVipID = modelPVip.PVipID, UserID = (int)Session["UserID"], PaymentID = 1 };
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
            return RedirectToAction("Index","Client");
        }
    }
}