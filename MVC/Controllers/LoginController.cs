using API.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LoginNormal(string usrMail,string usrPwd)
        {
            HttpResponseMessage res = APIService.client.GetAsync("Login?email=" + usrMail + "&pwd=" + usrPwd).Result;
            var model = res.Content.ReadAsAsync<UserView>().Result;
            if (res.IsSuccessStatusCode)
            {
                if (model.RoleID == 1)
                {
                    Session["ad"] = model.ID;
                    return RedirectToAction("Index", "Admin");
                }
                if (model.RoleID == 3)
                {
                    Session["UserID"] = model.ID;
                    return RedirectToAction("Index", "Client");
                }
            }
            TempData["error"] = "Không đúng email hoặc mật khẩu";
            return RedirectToAction("Index");
        }
        public ActionResult RegisterNormal(UserView u)
        {
            u.RoleID = 3;
            u.UserImage = "default.png";
            u.UserDescription = null;
            var res = APIService.client.PostAsJsonAsync("User", u).Result;
            if (res.IsSuccessStatusCode)
            {
                Session["UserID"] = res.Content.ReadAsAsync<int>().Result;
                return RedirectToAction("Index", "Client");
            }
            TempData["error"] = "Email này đã được đăng kí";
            return RedirectToAction("Index");
        }
        public ActionResult ChangePwd(string email)
        {
            var number = new Random().Next(1000, 9999);
            try
                {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("dtmt1610@gmail.com", "Nhaccuateo");
                    var receiverEmail = new MailAddress(email);
                    var password = "dtmt16101302";
                    var sub = "Change your password";
                    var body = "Mã số bí mật của bạn là: " + number + ".";
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 25,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receiverEmail)
                    {
                        Subject = sub,
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    Session["number"] = number;
                    return RedirectToAction("ChangePwd", "Client",new { mail=email});
                }
            }
                catch (Exception e)
            {
                ViewBag.Error = "Some Error";
                Console.WriteLine(e.Message);
            }
            TempData["error"] = "Không gửi đc mail";
            return RedirectToAction("Index");
        }
    }
}