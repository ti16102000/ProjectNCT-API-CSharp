using API.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class ClientController : Controller
    {
        //Menu
        public ActionResult GetCate()
        {
            HttpResponseMessage resAllCate = APIService.client.GetAsync("Category").Result;
            IEnumerable<CategoryView> ls = resAllCate.Content.ReadAsAsync<IEnumerable<CategoryView>>().Result;
            TempData["cate"] = ls;
            Session["UserID"] = 11;
            if (Session["UserID"] != null)
            {
                HttpResponseMessage res = APIService.client.GetAsync("User/" + Convert.ToInt32(Session["UserID"])).Result;
                UserView user = res.Content.ReadAsAsync<UserView>().Result;
                Session["Vip"] = user.UserVIP;
                ViewBag.usr = user;
            }
            return PartialView();
        }
        //Change Password
        public ActionResult ChangePwd(string mail)
        {
            ViewBag.mail = mail;
            return View();
        }
        public ActionResult NewPass(string email, string pwd)
        {
            HttpResponseMessage res = APIService.client.GetAsync("Login?mail=" + email + "&pwdNew=" + pwd).Result;
            if (res.IsSuccessStatusCode)
            {
                Session.Remove("number");
                return RedirectToAction("Index", "Login");
            }
            TempData["error"] = "Lỗi không đổi đc mật khẩu";
            return RedirectToAction("ChangePwd", new { mail = email });
        }
        // GET: Client
        #region Index
        public ActionResult Index()
        {
            HttpResponseMessage resSong = APIService.client.GetAsync("Music?music=" + true).Result;
            ViewBag.song = resSong.Content.ReadAsAsync<IEnumerable<MusicView>>().Result;
            HttpResponseMessage resMV = APIService.client.GetAsync("Music?music=" + false).Result;
            ViewBag.mv = resMV.Content.ReadAsAsync<IEnumerable<MusicView>>().Result;
            HttpResponseMessage resP = APIService.client.GetAsync("Partner").Result;
            ViewBag.p = resP.Content.ReadAsAsync<IEnumerable<PartnerView>>().Result;
            HttpResponseMessage resCate = APIService.client.GetAsync("SubCate/" + 5).Result;
            ViewBag.cate = resCate.Content.ReadAsAsync<IEnumerable<CategoryView>>().Result;
            return View();
        }
        #endregion
        #region Play Music
        public ActionResult PlayMusic(int idMusic)
        {
            //history
            if (Session["UserID"] != null)
            {
                var resHis = APIService.client.PostAsJsonAsync("HistoryUser", new HistoryUserView { MusicID = idMusic, UserID = Convert.ToInt32(Session["UserID"])});
            }
            //music
            HttpResponseMessage resView = APIService.client.GetAsync("Music?idMusic=" + idMusic).Result;
            HttpResponseMessage res = APIService.client.GetAsync("Music/" + idMusic).Result;
            MusicView model = res.Content.ReadAsAsync<MusicView>().Result;
            //lyrics
            HttpResponseMessage resLyrics = APIService.client.GetAsync("Lyrics/" + model.ID).Result;
            ViewBag.lyrics = resLyrics.Content.ReadAsAsync<LyricsView>().Result;
            //PlaylistUser
            if (Session["UserID"] != null)
            {
                HttpResponseMessage resPlist = APIService.client.GetAsync("Playlist?idUser=" + Convert.ToInt32(Session["UserID"])).Result;
                IEnumerable<PlaylistView> plist = resPlist.Content.ReadAsAsync<IEnumerable<PlaylistView>>().Result;
                if (plist.Count() > 0)
                {
                    ViewBag.plist = plist;
                }
                else
                {
                    ViewBag.plist = null;
                }
            }
            //file
            HttpResponseMessage resFileNormal = APIService.client.GetAsync("QualityMusic?idMusic=" + model.ID + "&vip=" + false).Result;
            ViewBag.fileNormal = resFileNormal.Content.ReadAsAsync<QualityMusicView>().Result;
            HttpResponseMessage resFile = APIService.client.GetAsync("QualityMusic?idMusic=" + model.ID).Result;
            ViewBag.file = resFile.Content.ReadAsAsync<IEnumerable<QualityMusicView>>().Result;
            //music random
            HttpResponseMessage resRandom = APIService.client.GetAsync("RandomMusic?idCate=" + model.CateID + "&music=" + model.SongOrMV).Result;
            ViewBag.random = resRandom.Content.ReadAsAsync<IEnumerable<MusicView>>().Result;
            return View(model);
        }
        [HttpPost]
        public JsonResult AddMusicPlaylist(PlaylistMusicView pl)
        {
            HttpResponseMessage res = APIService.client.PostAsJsonAsync("PlaylistMusic", pl).Result;
            if (res.IsSuccessStatusCode)
            {
                return Json(new
                {
                    status = true
                }, JsonRequestBehavior.AllowGet);
            }
            return Json(new
            {
                status = false
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult UpdateView(int idMusic)
        {
            HttpResponseMessage resView = APIService.client.GetAsync("Music?idMusic=" + idMusic).Result;
            int view = resView.Content.ReadAsAsync<int>().Result;
            return Json(new{data=view}, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddLyrics(LyricsView lv)
        {
            var res = APIService.client.PostAsJsonAsync("Lyrics", lv).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Đăng lời cho bài hát này thành công";
            }
            else
            {
                TempData["error"] = "Xảy ra lỗi khi đăng lời cho bài hát này";
            }
            return RedirectToAction("PlayMusic", new { idMusic = lv.MusicID });
        }
        public ActionResult UpdateLyrics(LyricsView lv)
        {
            var res = APIService.client.PutAsJsonAsync("Lyrics/" + lv.MusicID, lv).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Cập nhật lời cho bài hát này thành công";
            }
            else
            {
                TempData["error"] = "Xảy ra lỗi khi cập nhật lời cho bài hát này";
            }
            return RedirectToAction("PlayMusic", new { idMusic = lv.MusicID });
        }
        #endregion

        #region List Playlist By IDCate
        public ActionResult ListPlaylistByIDCate(int id,string name)
        {
            ViewBag.namecate = name;
            var res = APIService.client.GetAsync("Playlist?idCate=" + id).Result;
            ViewBag.plist = res.Content.ReadAsAsync<IEnumerable<PlaylistView>>().Result;
            return View();
        }
        #endregion

        #region Play Playlist
        public ActionResult PlayPlist()
        {
            return View();
        }
        #endregion
    }
}