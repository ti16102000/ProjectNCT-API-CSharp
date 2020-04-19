using API.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
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
                System.Threading.Tasks.Task<HttpResponseMessage> resHis = APIService.client.PostAsJsonAsync("HistoryUser", new HistoryUserView { MusicID = idMusic, UserID = Convert.ToInt32(Session["UserID"]) });
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
            return Json(new { data = view }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddLyrics(LyricsView lv)
        {
            HttpResponseMessage res = APIService.client.PostAsJsonAsync("Lyrics", lv).Result;
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
            HttpResponseMessage res = APIService.client.PutAsJsonAsync("Lyrics/" + lv.MusicID, lv).Result;
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
        public ActionResult ListPlaylistByIDCate(int id, string name)
        {
            ViewBag.namecate = name;
            HttpResponseMessage res = APIService.client.GetAsync("Playlist?idCate=" + id).Result;
            ViewBag.plist = res.Content.ReadAsAsync<IEnumerable<PlaylistView>>().Result;
            return View();
        }
        #endregion

        #region Play Playlist
        public ActionResult PlayPlist(int id)
        {
            HttpResponseMessage resPlist = APIService.client.GetAsync("Playlist/" + id).Result;
            PlaylistView model = resPlist.Content.ReadAsAsync<PlaylistView>().Result;
            //list music
            HttpResponseMessage resMusic = APIService.client.GetAsync("PlaylistMusic?idPlist=" + id).Result;
            ViewBag.music = resMusic.Content.ReadAsAsync<IEnumerable<MusicView>>().Result;
            //random playlist
            HttpResponseMessage resRandom = APIService.client.GetAsync("Playlist?idCate=" + model.CateID + "&number=" + 3).Result;
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
        public ActionResult ListMusicByIDCate(int id, string name, bool music)
        {
            HttpResponseMessage res = APIService.client.GetAsync("Music?idCate=" + id + "&music=" + music).Result;
            IEnumerable<MusicView> ls = res.Content.ReadAsAsync<IEnumerable<MusicView>>().Result;
            ViewBag.name = name;
            ViewBag.music = music;
            return View(ls);
        }
        #endregion

        #region Search
        [HttpGet]
        public JsonResult SearchMusicMenu(string value, bool music)
        {
            HttpResponseMessage res = APIService.client.GetAsync("SearchMenu?value=" + value + "&music=" + music).Result;
            IEnumerable<MusicView> ls = res.Content.ReadAsAsync<IEnumerable<MusicView>>().Result;
            return Json(new { lsData = ls }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult SearchSingerMenu(string value)
        {
            HttpResponseMessage res = APIService.client.GetAsync("SearchMenu?value=" + value).Result;
            IEnumerable<UserView> ls = res.Content.ReadAsAsync<IEnumerable<UserView>>().Result;
            return Json(new { lsData = ls }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Search(string value)
        {
            ViewBag.value = value;
            //song
            HttpResponseMessage resSong = APIService.client.GetAsync("Music?value=" + value + "&music=" + true).Result;
            ViewBag.song = resSong.Content.ReadAsAsync<IEnumerable<MusicView>>().Result;
            //mv
            HttpResponseMessage resMV = APIService.client.GetAsync("Music?value=" + value + "&music=" + false).Result;
            ViewBag.mv = resMV.Content.ReadAsAsync<IEnumerable<MusicView>>().Result;
            //singer
            HttpResponseMessage resSinger = APIService.client.GetAsync("User?value=" + value).Result;
            ViewBag.singer = resSinger.Content.ReadAsAsync<IEnumerable<UserView>>().Result;
            //playlist
            HttpResponseMessage resPlist = APIService.client.GetAsync("Playlist?value=" + value).Result;
            ViewBag.plist = resPlist.Content.ReadAsAsync<IEnumerable<PlaylistView>>().Result;
            return View();
        }
        #endregion

        #region Rank
        public ActionResult RankMusic(int idCate, string name)
        {
            ViewBag.name = name;
            //song
            HttpResponseMessage resSong = APIService.client.GetAsync("RankMusic?idCate=" + idCate + "&music=" + true).Result;
            RankView song = resSong.Content.ReadAsAsync<RankView>().Result;
            ViewBag.song = song;
            HttpResponseMessage resLsSong = APIService.client.GetAsync("RankMusic?idRank=" + song.ID).Result;
            ViewBag.lsSong = resLsSong.Content.ReadAsAsync<IEnumerable<RankMusicView>>().Result;
            //mv
            HttpResponseMessage resMv = APIService.client.GetAsync("RankMusic?idCate=" + idCate + "&music=" + false).Result;
            RankView mv = resMv.Content.ReadAsAsync<RankView>().Result;
            ViewBag.mv = mv;
            HttpResponseMessage resLsMV = APIService.client.GetAsync("RankMusic?idRank=" + mv.ID).Result;
            ViewBag.lsMv = resLsMV.Content.ReadAsAsync<IEnumerable<RankMusicView>>().Result;
            return View();
        }
        #endregion

        #region Upload file
        public ActionResult UploadFile()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Index", "Login");
            }
            HttpResponseMessage resCate = APIService.client.GetAsync("Category").Result;
            ViewBag.cate = resCate.Content.ReadAsAsync<IEnumerable<CategoryView>>().Result;
            Session.Remove("singer");
            return View();
        }
        [HttpGet]
        public JsonResult SearchSingerUpload(string value)
        {
            HttpResponseMessage resSinger = APIService.client.GetAsync("User?value=" + value).Result;
            IEnumerable<UserView> ls = resSinger.Content.ReadAsAsync<IEnumerable<UserView>>().Result;
            return Json(new { lsData = ls }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetListSinger(int number)
        {
            List<int> arr = new List<int>();
            if (Session["singer"] != null)
            {
                arr = Session["singer"] as List<int>;
                arr.Add(number);
                Session["singer"] = arr;
            }
            else
            {
                arr.Add(number);
                Session["singer"] = arr;
            }
            return Json(new { data = number }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult RemoveIdSinger(int id)
        {
            List<int> arr = new List<int>();
            arr = Session["singer"] as List<int>;
            arr.Remove(id);
            Session["singer"] = arr;
            return Json(new { data = id }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CreateMusicUser(MusicView m, HttpPostedFileBase fileMusic, HttpPostedFileBase imgMusic)
        {
            if (Session["singer"] != null)
            {
                List<int> arrSinger = new List<int>();
                arrSinger = Session["singer"] as List<int>;
                m.MusicNameUnsigned = API.CommonHelper.RemoveUnicode.RemoveSign4VietnameseString(m.MusicName);
                m.MusicDownloadAllowed = true;
                m.View = 0;
                //img music
                if (imgMusic == null)
                {
                    m.MusicImage = "default.png";
                }
                else
                {
                    string FileNameMusic = DateTime.Now.Ticks + Path.GetFileName(imgMusic.FileName);
                    string pathMusic = Path.Combine(Server.MapPath("~/Resource/ImagesMusic"), FileNameMusic);
                    imgMusic.SaveAs(pathMusic);
                    m.MusicImage = FileNameMusic;
                }
                //quality music
                QualityMusicView quality = new QualityMusicView
                {
                    NewFile = true,
                    QMusicApproved = false //if admin do not approved file user, delete it(include singermusic, qualitymusic, music) without QMusicApproved is false
                };
                string FileName = DateTime.Now.Ticks + Path.GetFileName(fileMusic.FileName);
                string path;
                if (FileName.EndsWith("mp3"))
                {
                    quality.QualityID = 1; //file normal of song
                    m.SongOrMV = true;
                    path = Path.Combine(Server.MapPath("~/Resource/Audio"), FileName);
                }
                else
                {
                    quality.QualityID = 3; //file mormal of mv
                    m.SongOrMV = false;
                    path = Path.Combine(Server.MapPath("~/Resource/Video"), FileName);
                }
                fileMusic.SaveAs(path);
                quality.MusicFile = FileName;
                //music
                HttpResponseMessage resMusic = APIService.client.PostAsJsonAsync("Music", m).Result;
                if (resMusic.IsSuccessStatusCode)
                {
                    int idMusic = resMusic.Content.ReadAsAsync<int>().Result;
                    quality.MusicID = idMusic;
                    HttpResponseMessage res = APIService.client.PostAsJsonAsync("QualityMusic", quality).Result;
                    if (res.IsSuccessStatusCode)
                    {
                        //singer
                        foreach (int number in arrSinger)
                        {
                            System.Threading.Tasks.Task<HttpResponseMessage> resSM = APIService.client.PostAsJsonAsync("SingerMusic", new SingerMusicView { MusicID = idMusic, SingerID = number });
                        }
                    }

                    TempData["success"] = "Upload file thành công";
                }
            }
            else
            {
                TempData["error"] = "Upload file xảy ra lỗi";
            }
            Session.Remove("singer");
            return RedirectToAction("UploadFile");
        }
        #endregion

        #region Personal Page
        public ActionResult PersonalUser(int id)
        {
            HttpResponseMessage res = APIService.client.GetAsync("User/" + id).Result;
            UserView model = res.Content.ReadAsAsync<UserView>().Result;
            //playlist
            HttpResponseMessage resPlist = APIService.client.GetAsync("Playlist?idUser=" + id).Result;
            ViewBag.plist = resPlist.Content.ReadAsAsync<IEnumerable<PlaylistView>>().Result;
            //file upload

            return View(model);
        }
        #endregion
    }
}