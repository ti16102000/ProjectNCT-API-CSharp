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
            Session["UserID"] = 11; //auto login acc user have id=11(data in my sql) to test my web faster
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
        public ActionResult PlayPlist(int id)
        {
            var resPlist = APIService.client.GetAsync("Playlist/" + id).Result;
            var model = resPlist.Content.ReadAsAsync<PlaylistView>().Result;
            //list music
            var resMusic = APIService.client.GetAsync("PlaylistMusic?idPlist=" + id).Result;
            ViewBag.music = resMusic.Content.ReadAsAsync<IEnumerable<MusicView>>().Result;
            //random playlist
            var resRandom = APIService.client.GetAsync("Playlist?idCate=" + model.CateID + "&number=" + 3).Result;
            ViewBag.random = resRandom.Content.ReadAsAsync<IEnumerable<PlaylistView>>().Result;
            //PlaylistUser
            if (Session["UserID"] != null)
            {
                HttpResponseMessage res = APIService.client.GetAsync("Playlist?idUser=" + Convert.ToInt32(Session["UserID"])).Result;
                IEnumerable<PlaylistView> plist = res.Content.ReadAsAsync<IEnumerable<PlaylistView>>().Result;
                if (plist.Count() > 0)
                {
                    ViewBag.plist = plist;
                }
                else
                {
                    ViewBag.plist = null;
                }
            }
            return View(model);
        }
        #endregion

        #region List Music By IDCate
        public ActionResult ListMusicByIDCate(int id,string name,bool music)
        {
            var res = APIService.client.GetAsync("Music?idCate=" + id + "&music=" + music).Result;
            var ls = res.Content.ReadAsAsync<IEnumerable<MusicView>>().Result;
            ViewBag.name = name;
            ViewBag.music = music;
            return View(ls);
        }
        #endregion

        #region Search
        [HttpGet]
        public JsonResult SearchMusicMenu(string value,bool music)
        {
            var res = APIService.client.GetAsync("SearchMenu?value=" + value + "&music=" + music).Result;
            var ls = res.Content.ReadAsAsync<IEnumerable<MusicView>>().Result;
            return Json(new { lsData = ls }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult SearchSingerMenu(string value)
        {
            var res = APIService.client.GetAsync("SearchMenu?value=" + value).Result;
            var ls = res.Content.ReadAsAsync<IEnumerable<UserView>>().Result;
            return Json(new { lsData = ls }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string value)
        {
            ViewBag.value = value;
            //song
            var resSong = APIService.client.GetAsync("Music?value=" + value + "&music=" + true).Result;
            ViewBag.song = resSong.Content.ReadAsAsync<IEnumerable<MusicView>>().Result;
            //mv
            var resMV = APIService.client.GetAsync("Music?value=" + value + "&music=" + false).Result;
            ViewBag.mv = resMV.Content.ReadAsAsync<IEnumerable<MusicView>>().Result;
            //singer
            var resSinger = APIService.client.GetAsync("User?value=" + value).Result;
            ViewBag.singer = resSinger.Content.ReadAsAsync<IEnumerable<UserView>>().Result;
            //playlist
            var resPlist = APIService.client.GetAsync("Playlist?value=" + value).Result;
            ViewBag.plist = resPlist.Content.ReadAsAsync<IEnumerable<PlaylistView>>().Result;
            return View();
        }
        #endregion

        #region Rank
        public ActionResult RankMusic(int idCate,string name)
        {
            ViewBag.name = name;
            //song
            var resSong = APIService.client.GetAsync("RankMusic?idCate=" + idCate + "&music=" + true).Result;
            var song = resSong.Content.ReadAsAsync<RankView>().Result;
            ViewBag.song = song;
            var resLsSong = APIService.client.GetAsync("RankMusic?idRank=" + song.ID).Result;
            ViewBag.lsSong = resLsSong.Content.ReadAsAsync<IEnumerable<RankMusicView>>().Result;
            //mv
            var resMv = APIService.client.GetAsync("RankMusic?idCate=" + idCate + "&music=" + false).Result;
            var mv = resMv.Content.ReadAsAsync<RankView>().Result;
            ViewBag.mv = mv;
            var resLsMV = APIService.client.GetAsync("RankMusic?idRank=" + mv.ID).Result;
            ViewBag.lsMv = resLsMV.Content.ReadAsAsync<IEnumerable<RankMusicView>>().Result;
            return View();
        }
        #endregion
    }
}