using API.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVC.Controllers
{
    public class AdminController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            object o = filterContext.HttpContext.Session["ad"];
            //var actionName = filterContext.RouteData.Values["action"];
            //var ControllerName = filterContext.RouteData.Values["controller"];
            if (o == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "Controller", "Client" }, { "Action", "Index" } }); //back to public page
            }

        }

        // GET: Admin
        #region New file
        public ActionResult Index()
        {
            HttpResponseMessage res = APIService.client.GetAsync("NewFile").Result;
            IEnumerable<MusicView> ls = res.Content.ReadAsAsync<IEnumerable<MusicView>>().Result;
            return View(ls);
        }
        public ActionResult NewFileDetail(int id)
        {
            HttpResponseMessage res = APIService.client.GetAsync("QualityMusic?idMusic=" + id + "&vip=" + false).Result;
            QualityMusicView model = res.Content.ReadAsAsync<QualityMusicView>().Result;
            return View(model);
        }
        public ActionResult ApproveFile(int id)
        {
            HttpResponseMessage res = APIService.client.GetAsync("NewFile/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Duyệt thành công";
            }
            else
            {
                TempData["error"] = "Duyệt xảy ra lỗi";
            }
            return RedirectToAction("Index");
        }
        public ActionResult NotApproveFile(int id)
        {
            HttpResponseMessage resFile = APIService.client.GetAsync("QualityMusic?idMusic=" + id + "&vip=" + false).Result;
            QualityMusicView model = resFile.Content.ReadAsAsync<QualityMusicView>().Result;
            if (model.ItemMusic.MusicImage != "default.png")
            {
                string fullPath = Request.MapPath("~/Resource/ImagesMusic/" + model.ItemMusic.MusicImage);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            if (model.ItemMusic.SongOrMV == true)
            {
                string fullPathAudio = Request.MapPath("~/Resource/Audio/" + model.MusicFile);
                System.IO.File.Delete(fullPathAudio);
            }
            else
            {
                string fullPathVideo = Request.MapPath("~/Resource/Video/" + model.MusicFile);
                System.IO.File.Delete(fullPathVideo);
            }
            HttpResponseMessage res = APIService.client.DeleteAsync("NewFile/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Xóa thành công";
            }
            else
            {
                TempData["error"] = "Xóa xảy ra lỗi";
            }
            return RedirectToAction("Index");
        }
        #endregion

        #region Category
        public ActionResult Category()
        {
            HttpResponseMessage res = APIService.client.GetAsync("Category").Result;
            IEnumerable<CategoryView> ls = res.Content.ReadAsAsync<IEnumerable<CategoryView>>().Result;
            return View(ls);
        }
        public ActionResult CreateCate(CategoryView cate)
        {
            HttpResponseMessage res = APIService.client.PostAsJsonAsync("Category", cate).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Create new category successfully!";
            }
            else
            {
                TempData["error"] = "Create new category failed!";
            }
            return RedirectToAction("Category");
        }
        public ActionResult EditCate(int id)
        {

            HttpResponseMessage res = APIService.client.GetAsync("Category/" + id).Result;
            CategoryView cate = res.Content.ReadAsAsync<CategoryView>().Result;
            HttpResponseMessage res1 = APIService.client.GetAsync("Category").Result;
            TempData["cate"] = res1.Content.ReadAsAsync<IEnumerable<CategoryView>>().Result;
            return View(cate);
        }
        public ActionResult UpdateCate(CategoryView cate)
        {
            HttpResponseMessage res = APIService.client.PutAsJsonAsync("Category/" + cate.ID, cate).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Update category successfully!";
            }
            else
            {
                TempData["error"] = "Update category failed!";
            }
            return RedirectToAction("Category");

        }
        public ActionResult ListSubCate(int id, string name)
        {
            TempData["name"] = name;
            HttpResponseMessage res = APIService.client.GetAsync("Category?idroot=" + id).Result;
            IEnumerable<CategoryView> ls = res.Content.ReadAsAsync<IEnumerable<CategoryView>>().Result;
            return View(ls);
        }
        #endregion

        #region Quality
        public ActionResult Quality()
        {
            HttpResponseMessage res = APIService.client.GetAsync("Quality").Result;
            IEnumerable<QualityMusicView> ls = res.Content.ReadAsAsync<IEnumerable<QualityMusicView>>().Result;
            return View(ls);
        }
        public ActionResult CreateQuality(QualityMusicView qv)
        {
            HttpResponseMessage res = APIService.client.PostAsJsonAsync("Quality", qv).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Create new quality successfully!";
            }
            else
            {
                TempData["error"] = "Create new quality failed!";
            }
            return RedirectToAction("Quality");
        }
        public ActionResult EditQuality(int id)
        {
            HttpResponseMessage res = APIService.client.GetAsync("Quality/" + id).Result;
            QualityMusicView ls = res.Content.ReadAsAsync<QualityMusicView>().Result;
            return View(ls);
        }
        public ActionResult UpdateQuality(QualityMusicView qv)
        {
            HttpResponseMessage res = APIService.client.PutAsJsonAsync("Quality/" + qv.QualityID, qv).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Update quality successfully!";
            }
            else
            {
                TempData["error"] = "Update quality failed!";
            }
            return RedirectToAction("Quality");
        }
        #endregion

        #region Singer
        public ActionResult ListSinger()
        {
            HttpResponseMessage res = APIService.client.GetAsync("User").Result;
            IEnumerable<UserView> ls = res.Content.ReadAsAsync<IEnumerable<UserView>>().Result;
            return View(ls);
        }
        public ActionResult Singer()
        {
            return View();
        }
        public ActionResult CreateSinger(UserView u, HttpPostedFileBase imgUser)
        {
            u.UserNameUnsigned = API.CommonHelper.RemoveUnicode.RemoveSign4VietnameseString(u.UserName);
            u.RoleID = 2;
            if (imgUser == null)
            {
                u.UserImage = "default.png";
            }
            else
            {
                string FileName = DateTime.Now.Ticks + Path.GetFileName(imgUser.FileName);
                string path = Path.Combine(Server.MapPath("~/Resource/ImagesUser"), FileName);
                imgUser.SaveAs(path);
                u.UserImage = FileName;
            }
            HttpResponseMessage res = APIService.client.PostAsJsonAsync("User", u).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Create new singer successfully!";
            }
            else
            {
                TempData["error"] = "Create new singer failed!";
            }
            return RedirectToAction("ListSinger");
        }
        public ActionResult SingerDetail(int id)
        {
            HttpResponseMessage res = APIService.client.GetAsync("User/" + id).Result;
            UserView model = res.Content.ReadAsAsync<UserView>().Result;
            HttpResponseMessage resPlaylist = APIService.client.GetAsync("Playlist?idUser=" + id).Result;
            TempData["pl"] = resPlaylist.Content.ReadAsAsync<IEnumerable<PlaylistView>>().Result;
            HttpResponseMessage resSong = APIService.client.GetAsync("Music?idSinger=" + id + "&music="+true).Result;
            TempData["song"] = resSong.Content.ReadAsAsync<IEnumerable<MusicView>>().Result;
            HttpResponseMessage resMV = APIService.client.GetAsync("Music?idSinger=" + id + "&music=" + false).Result;
            TempData["mv"] = resMV.Content.ReadAsAsync<IEnumerable<MusicView>>().Result;
            return View(model);
        }
        public ActionResult UpdateSinger(UserView u, HttpPostedFileBase imgUser)
        {
            u.UserNameUnsigned = API.CommonHelper.RemoveUnicode.RemoveSign4VietnameseString(u.UserName);
            HttpResponseMessage resUser = APIService.client.GetAsync("User/" + u.ID).Result;
            UserView model = resUser.Content.ReadAsAsync<UserView>().Result;
            if (imgUser == null)
            {
                u.UserImage = model.UserImage;
            }
            else
            {
                if (model.UserImage != "default.png")
                {
                    string fullPath = Request.MapPath("~/Resource/ImagesUser/" + model.UserImage);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
                string FileName = DateTime.Now.Ticks + Path.GetFileName(imgUser.FileName);
                string path = Path.Combine(Server.MapPath("~/Resource/ImagesUser"), FileName);
                imgUser.SaveAs(path);
                u.UserImage = FileName;
            }
            HttpResponseMessage res = APIService.client.PutAsJsonAsync("User/" + u.ID, u).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Update singer successfully!";
            }
            else
            {
                TempData["error"] = "Update singer failed!";
            }
            return RedirectToAction("SingerDetail", new { id = u.ID });
        }
        #endregion

        #region Playlist
        public ActionResult Playlist()
        {
            HttpResponseMessage resCate = APIService.client.GetAsync("SubCate").Result;
            TempData["cate"] = resCate.Content.ReadAsAsync<IEnumerable<CategoryView>>().Result;
            HttpResponseMessage resSinger = APIService.client.GetAsync("User").Result;
            TempData["singer"] = resSinger.Content.ReadAsAsync<IEnumerable<UserView>>().Result;
            return View();
        }
        public ActionResult CreatePlaylist(PlaylistView p, HttpPostedFileBase imgPlaylist)
        {
            if (imgPlaylist == null)
            {
                p.PlaylistImage = "default.png";
            }
            else
            {
                string FileName = DateTime.Now.Ticks + Path.GetFileName(imgPlaylist.FileName);
                string path = Path.Combine(Server.MapPath("~/Resource/ImagesMusic"), FileName);
                imgPlaylist.SaveAs(path);
                p.PlaylistImage = FileName;
            }
            HttpResponseMessage res = APIService.client.PostAsJsonAsync("Playlist", p).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Create new playlist successfully!";

            }
            else
            {
                TempData["error"] = "Create new playlist failed!";
            }
            return RedirectToAction("Playlist");
        }
        public ActionResult PlaylistDetail(int id)
        {
            HttpResponseMessage resCate = APIService.client.GetAsync("SubCate").Result;
            TempData["cate"] = resCate.Content.ReadAsAsync<IEnumerable<CategoryView>>().Result;
            HttpResponseMessage resSinger = APIService.client.GetAsync("User").Result;
            TempData["singer"] = resSinger.Content.ReadAsAsync<IEnumerable<UserView>>().Result;
            HttpResponseMessage res = APIService.client.GetAsync("Playlist/" + id).Result;
            PlaylistView model = res.Content.ReadAsAsync<PlaylistView>().Result;
            HttpResponseMessage resMV = APIService.client.GetAsync("Music?idSinger=" + model.UserID + "&music=" + true).Result;
            TempData["music"] = resMV.Content.ReadAsAsync<IEnumerable<MusicView>>().Result;
            HttpResponseMessage resPM = APIService.client.GetAsync("PlaylistMusic/" + id).Result;
            TempData["pm"] = resPM.Content.ReadAsAsync<IEnumerable<PlaylistMusicView>>().Result;
            return View(model);
        }
        public ActionResult UpdatePlaylist(PlaylistView p, HttpPostedFileBase imgPlaylist)
        {
            HttpResponseMessage resP = APIService.client.GetAsync("Playlist/" + p.ID).Result;
            PlaylistView model = resP.Content.ReadAsAsync<PlaylistView>().Result;
            if (imgPlaylist == null)
            {
                p.PlaylistImage = model.PlaylistImage;
            }
            else
            {
                if (model.PlaylistImage != "default.png")
                {
                    string fullPath = Request.MapPath("~/Resource/ImagesMusic/" + model.PlaylistImage);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                        
                    }
                }
                string FileName = DateTime.Now.Ticks + Path.GetFileName(imgPlaylist.FileName);
                string path = Path.Combine(Server.MapPath("~/Resource/ImagesMusic"), FileName);
                imgPlaylist.SaveAs(path);
                p.PlaylistImage = FileName;

            }
            HttpResponseMessage res = APIService.client.PutAsJsonAsync("Playlist/" + p.ID, p).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Update playlist successfully!";
            }
            else
            {
                TempData["error"] = "Update playlist failed!";
            }
            return RedirectToAction("PlaylistDetail", new { id = p.ID });
        }
        #endregion

        #region PlaylistMusic
        public ActionResult AddMusicPlaylist(PlaylistMusicView pm)
        {
            HttpResponseMessage res = APIService.client.PostAsJsonAsync("PlaylistMusic", pm).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Add music to playlist successfully!";
            }
            else
            {
                TempData["error"] = "Add music to playlist failed!";
            }
            return RedirectToAction("PlaylistDetail", new { id = pm.PlaylistID });
        }
        public ActionResult DelPM(int id, int idPL)
        {
            HttpResponseMessage res = APIService.client.DeleteAsync("PlaylistMusic/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Delete music playlist successfully!";
            }
            else
            {
                TempData["error"] = "Delete music playlist failed!";
            }
            return RedirectToAction("PlaylistDetail", new { id = idPL });
        }
        #endregion

        #region Music
        public ActionResult Music()
        {
            HttpResponseMessage resCate = APIService.client.GetAsync("SubCate").Result;
            TempData["cate"] = resCate.Content.ReadAsAsync<IEnumerable<CategoryView>>().Result;
            HttpResponseMessage resSinger = APIService.client.GetAsync("User").Result;
            TempData["singer"] = resSinger.Content.ReadAsAsync<IEnumerable<UserView>>().Result;
            return View();
        }
        public ActionResult CreateMusic(MusicView m, HttpPostedFileBase imgMusic)
        {
            Random rnd = new Random();
            m.View = rnd.Next(1000, 9999);
            m.MusicNameUnsigned = API.CommonHelper.RemoveUnicode.RemoveSign4VietnameseString(m.MusicName);
            if (imgMusic == null)
            {
                m.MusicImage = "default.png";
            }
            else
            {
                string FileName = DateTime.Now.Ticks + Path.GetFileName(imgMusic.FileName);
                string path = Path.Combine(Server.MapPath("~/Resource/ImagesMusic"), FileName);
                imgMusic.SaveAs(path);
                m.MusicImage = FileName;
            }
            HttpResponseMessage res = APIService.client.PostAsJsonAsync("Music", m).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Create new music successfully!";
                int id = res.Content.ReadAsAsync<int>().Result;
                System.Threading.Tasks.Task<HttpResponseMessage> resSM = APIService.client.PostAsJsonAsync("SingerMusic", new SingerMusicView { MusicID = id, SingerID = m.UserID });
            }
            else
            {
                TempData["error"] = "Create new music failed!";
            }
            return RedirectToAction("Music");
        }
        public ActionResult MusicDetail(int id)
        {
            HttpResponseMessage resCate = APIService.client.GetAsync("SubCate").Result;
            TempData["cate"] = resCate.Content.ReadAsAsync<IEnumerable<CategoryView>>().Result;
            HttpResponseMessage res = APIService.client.GetAsync("Music/" + id).Result;
            MusicView model = res.Content.ReadAsAsync<MusicView>().Result;
            HttpResponseMessage resMV = APIService.client.GetAsync("Music?idSinger=" + model.UserID + "&music=" + (model.SongOrMV == true ? false : true)).Result;
            TempData["music"] = resMV.Content.ReadAsAsync<IEnumerable<MusicView>>().Result;
            HttpResponseMessage resSinger = APIService.client.GetAsync("User").Result;
            TempData["singer"] = resSinger.Content.ReadAsAsync<IEnumerable<UserView>>().Result;
            HttpResponseMessage resSm = APIService.client.GetAsync("SingerMusic/" + id).Result;
            TempData["sm"] = resSm.Content.ReadAsAsync<IEnumerable<SingerMusicView>>().Result;
            HttpResponseMessage resQ = APIService.client.GetAsync("Quality").Result;
            TempData["q"] = resQ.Content.ReadAsAsync<IEnumerable<QualityMusicView>>().Result;
            HttpResponseMessage resQm = APIService.client.GetAsync("QualityMusic?idMusic=" + id).Result;
            TempData["qm"] = resQm.Content.ReadAsAsync<IEnumerable<QualityMusicView>>().Result;
            return View(model);
        }
        public ActionResult UpdateMusic(MusicView m, HttpPostedFileBase imgMusic)
        {
            m.MusicNameUnsigned = API.CommonHelper.RemoveUnicode.RemoveSign4VietnameseString(m.MusicName);
            HttpResponseMessage res = APIService.client.GetAsync("Music/" + m.ID).Result;
            MusicView model = res.Content.ReadAsAsync<MusicView>().Result;
            if (imgMusic == null)
            {
                m.MusicImage = model.MusicImage;
            }
            else
            {
                if (model.MusicImage != "default.png")
                {
                    string fullPath = Request.MapPath("~/Resource/ImagesMusic/" + model.MusicImage);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
                string FileName = DateTime.Now.Ticks + Path.GetFileName(imgMusic.FileName);
                string path = Path.Combine(Server.MapPath("~/Resource/ImagesMusic"), FileName);
                imgMusic.SaveAs(path);
                m.MusicImage = FileName;
            }
            HttpResponseMessage resUp = APIService.client.PutAsJsonAsync("Music/" + m.ID, m).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Update music successfully!";
            }
            else
            {
                TempData["error"] = "Update music failed!";
            }
            return RedirectToAction("MusicDetail", new { id = m.ID });
        }
        public ActionResult AddMusicRelated(int id, int idMusicRelated)
        {
            HttpResponseMessage res1 = APIService.client.GetAsync("Music?idMusic=" + id + "&idRelated=" + idMusicRelated).Result;
            if (res1.IsSuccessStatusCode)
            {
                TempData["success"] = "add music related successfully!";
                HttpResponseMessage res2 = APIService.client.GetAsync("Music?idMusic=" + idMusicRelated + "&idRelated=" + id).Result;
            }
            else
            {
                TempData["error"] = "add music related failed!";
            }
            return RedirectToAction("MusicDetail", new { id = id });
        }
        public ActionResult RemoveRelated(int id, int idMusicRelated)
        {
            HttpResponseMessage res1 = APIService.client.GetAsync("Music?idMusic=" + id + "&idRelated=0").Result;
            if (res1.IsSuccessStatusCode)
            {
                TempData["success"] = "remove music related successfully!";
                HttpResponseMessage res2 = APIService.client.GetAsync("Music?idMusic=" + idMusicRelated + "&idRelated=0").Result;
            }
            else
            {
                TempData["error"] = "remove music related failed!";
            }
            return RedirectToAction("MusicDetail", new { id = id });
        }
        #endregion

        #region SingerMusic
        public ActionResult AddSingerMusic(SingerMusicView sm)
        {
            HttpResponseMessage res = APIService.client.PostAsJsonAsync("SingerMusic", sm).Result;
            HttpResponseMessage res1 = APIService.client.GetAsync("Music/" + sm.MusicID).Result;
            MusicView model = res.Content.ReadAsAsync<MusicView>().Result;

            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "add singer successfully!";
            }
            else
            {
                TempData["error"] = "add singer failed!";
            }
            return RedirectToAction("MusicDetail", new { id = sm.MusicID });
        }
        public ActionResult DelSinger(int id, int idMusic)
        {
            HttpResponseMessage res = APIService.client.DeleteAsync("SingerMusic/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "delete singer successfully!";
            }
            else
            {
                TempData["error"] = "delete singer failed!";
            }
            return RedirectToAction("MusicDetail", new { id = idMusic });
        }
        #endregion

        #region QualityMusic
        public ActionResult AddFile(QualityMusicView qm, HttpPostedFileBase fileMusic)
        {
            qm.QMusicApproved = true;
            qm.NewFile = false;
            HttpResponseMessage resModel = APIService.client.GetAsync("Music/" + qm.MusicID).Result;
            MusicView model = resModel.Content.ReadAsAsync<MusicView>().Result;
            if (fileMusic == null)
            {
                TempData["error"] = "add file failed!";
                return RedirectToAction("MusicDetail", new { id = qm.MusicID });
            }
            string FileName = DateTime.Now.Ticks + Path.GetFileName(fileMusic.FileName);
            string path;
            if ((model.SongOrMV == false && FileName.EndsWith("mp3")) || (model.SongOrMV == true && !FileName.EndsWith("mp3")))
            {
                TempData["error"] = "add file failed!";
                return RedirectToAction("MusicDetail", new { id = qm.MusicID });
            }
            if (FileName.EndsWith("mp3"))
            {
                path = Path.Combine(Server.MapPath("~/Resource/Audio"), FileName);
            }
            else
            {
                path = Path.Combine(Server.MapPath("~/Resource/Video"), FileName);
            }
            fileMusic.SaveAs(path);
            qm.MusicFile = FileName;
            HttpResponseMessage res = APIService.client.PostAsJsonAsync("QualityMusic", qm).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "add file successfully!";
            }
            else
            {
                TempData["error"] = "add file failed!";
            }
            return RedirectToAction("MusicDetail", new { id = qm.MusicID });
        }
        public ActionResult DelFile(int id)
        {
            HttpResponseMessage res = APIService.client.GetAsync("QualityMusic/" + id).Result;
            QualityMusicView model = res.Content.ReadAsAsync<QualityMusicView>().Result;
            if (model != null)
            {
                string fullPathAudio = Request.MapPath("~/Resource/Audio/" + model.MusicFile);
                string fullPathVideo = Request.MapPath("~/Resource/Video/" + model.MusicFile);
                if (System.IO.File.Exists(fullPathAudio))
                {
                    System.IO.File.Delete(fullPathAudio);
                }
                else
                {
                    System.IO.File.Delete(fullPathVideo);
                }
                HttpResponseMessage resDel = APIService.client.DeleteAsync("QualityMusic/" + id).Result;
                if (res.IsSuccessStatusCode)
                {
                    TempData["success"] = "delete file successfully!";
                }
            }
            else
            {
                TempData["error"] = "delete file failed!";
            }
            return RedirectToAction("MusicDetail", new { id = model.MusicID });
        }
        #endregion

        #region Partner
        public ActionResult Partner()
        {
            HttpResponseMessage res = APIService.client.GetAsync("Partner").Result;
            IEnumerable<PartnerView> ls = res.Content.ReadAsAsync<IEnumerable<PartnerView>>().Result;
            return View(ls);
        }
        public ActionResult CreatePartner(HttpPostedFileBase imgPartner, PartnerView p)
        {
            if (imgPartner == null)
            {
                p.PartnerImage = "default.png";
            }
            else
            {
                string FileName = DateTime.Now.Ticks + Path.GetFileName(imgPartner.FileName);
                string path = Path.Combine(Server.MapPath("~/Resource/ImagesUser"), FileName);
                imgPartner.SaveAs(path);
                p.PartnerImage = FileName;
            }
            HttpResponseMessage res = APIService.client.PostAsJsonAsync("Partner", p).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Create new partner successfully!";
            }
            else
            {
                TempData["error"] = "Create new partner failed!";
            }
            return RedirectToAction("Partner");
        }
        public ActionResult DelPartner(int id)
        {
            HttpResponseMessage resGet = APIService.client.GetAsync("Partner/" + id).Result;
            PartnerView model = resGet.Content.ReadAsAsync<PartnerView>().Result;
            if (model.PartnerImage != "default.png")
            {
                string fullPath = Request.MapPath("~/Resource/ImagesUser/" + model.PartnerImage);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            HttpResponseMessage resDel = APIService.client.DeleteAsync("Partner/" + id).Result;
            if (resDel.IsSuccessStatusCode)
            {
                TempData["success"] = "delete partner successfully!";
            }
            else
            {
                TempData["error"] = "delete partner failed!";
            }
            return RedirectToAction("Partner");
        }
        #endregion

        #region payment&packagevip
        public ActionResult Payment()
        {
            HttpResponseMessage resP = APIService.client.GetAsync("Payment").Result;
            TempData["p"] = resP.Content.ReadAsAsync<IEnumerable<PaymentView>>().Result;
            HttpResponseMessage resPV = APIService.client.GetAsync("PVip").Result;
            TempData["pv"] = resPV.Content.ReadAsAsync<IEnumerable<PackageVipView>>().Result;
            return View();
        }
        public ActionResult CreatePayment(PaymentView p)
        {
            HttpResponseMessage res = APIService.client.PostAsJsonAsync("Payment", p).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Create new payment successfully!";
            }
            else
            {
                TempData["error"] = "Create new payment failed!";
            }
            return RedirectToAction("Payment");
        }
        public ActionResult DelPayment(int id)
        {
            HttpResponseMessage res = APIService.client.DeleteAsync("Payment/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "delete payment successfully!";
            }
            else
            {
                TempData["error"] = "delete payment failed!";
            }
            return RedirectToAction("Payment");
        }
        public ActionResult CreatePV(PackageVipView pv)
        {
            HttpResponseMessage res = APIService.client.PostAsJsonAsync("PVip", pv).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Create new package vip successfully!";
            }
            else
            {
                TempData["error"] = "Create new package vip failed!";
            }
            return RedirectToAction("Payment");
        }
        public ActionResult DelPV(int id)
        {
            HttpResponseMessage res = APIService.client.DeleteAsync("PVip/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "delete payment successfully!";
            }
            else
            {
                TempData["error"] = "delete payment failed!";
            }
            return RedirectToAction("Payment");
        }
        #endregion

        #region Rank
        public ActionResult Rank()
        {
            //cate
            HttpResponseMessage resCate = APIService.client.GetAsync("Category").Result;
            TempData["cate"] = resCate.Content.ReadAsAsync<IEnumerable<CategoryView>>().Result;
            //rank song
            HttpResponseMessage resS = APIService.client.GetAsync("RankMusic?musicRank=" + true).Result;
            ViewBag.song = resS.Content.ReadAsAsync<IEnumerable<RankView>>().Result;
            //rank mv
            HttpResponseMessage resM = APIService.client.GetAsync("RankMusic?musicRank=" + false).Result;
            ViewBag.mv = resM.Content.ReadAsAsync<IEnumerable<RankView>>().Result;
            return View();
        }
        public ActionResult CreateRank(RankView r)
        {
            HttpResponseMessage res = APIService.client.PostAsJsonAsync("RankMusic", r).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "Create new Rank successfully!";
            }
            else
            {
                TempData["error"] = "Create new Rank failed!";
            }
            return RedirectToAction("Rank");
        }
        public ActionResult ListRM(int id)
        {
            HttpResponseMessage res = APIService.client.GetAsync("RankMusic/" + id).Result;
            RankView model = res.Content.ReadAsAsync<RankView>().Result;
            HttpResponseMessage resLs = APIService.client.GetAsync("RankMusic?idRank=" + id).Result;
            ViewBag.ls = resLs.Content.ReadAsAsync<IEnumerable<RankMusicView>>().Result;
            return View(model);
        }
        public ActionResult DelRank(int id)
        {
            HttpResponseMessage res = APIService.client.DeleteAsync("RankMusic/" + id).Result;
            if (res.IsSuccessStatusCode)
            {
                TempData["success"] = "delete Rank successfully!";
            }
            else
            {
                TempData["error"] = "delete Rank failed!";
            }
            return RedirectToAction("Rank");
        }
        #endregion

        #region User
        public ActionResult UserNormal()
        {
            var res = APIService.client.GetAsync("User?vip=" + false).Result;
            var ls = res.Content.ReadAsAsync<IEnumerable<UserView>>().Result;
            return View(ls);
        }
        public ActionResult UserVip()
        {
            var res = APIService.client.GetAsync("User?vip=" + true).Result;
            var ls = res.Content.ReadAsAsync<IEnumerable<UserView>>().Result;
            return View(ls);
        }
        public ActionResult UserDetail(int id)
        {
            HttpResponseMessage res = APIService.client.GetAsync("User/" + id).Result;
            UserView model = res.Content.ReadAsAsync<UserView>().Result;
            HttpResponseMessage resPlaylist = APIService.client.GetAsync("Playlist?idUser=" + id).Result;
            TempData["pl"] = resPlaylist.Content.ReadAsAsync<IEnumerable<PlaylistView>>().Result;
            //song
            var resSong = APIService.client.GetAsync("Music?idUser=" + id + "&music=" + true).Result; ;
            TempData["song"] = resSong.Content.ReadAsAsync<IEnumerable<MusicView>>().Result;
            //mv
            var resMV = APIService.client.GetAsync("Music?idUser=" + id + "&music=" + false).Result; ;
            TempData["mv"] = resMV.Content.ReadAsAsync<IEnumerable<MusicView>>().Result;
            //order
            var resOrd = APIService.client.GetAsync("Order/" + id).Result;
            TempData["ord"] = resOrd.Content.ReadAsAsync<IEnumerable<OrderView>>().Result;
            return View(model);
        }
        public ActionResult PlaylistDetailUser(int id)
        {
            HttpResponseMessage res = APIService.client.GetAsync("Playlist/" + id).Result;
            PlaylistView model = res.Content.ReadAsAsync<PlaylistView>().Result;
            HttpResponseMessage resPM = APIService.client.GetAsync("PlaylistMusic/" + id).Result;
            TempData["pm"] = resPM.Content.ReadAsAsync<IEnumerable<PlaylistMusicView>>().Result;
            return View(model);
        }
        public ActionResult MusicDetailUser(int id)
        {
            HttpResponseMessage res = APIService.client.GetAsync("QualityMusic?idMusic=" + id + "&vip=" + false).Result;
            QualityMusicView model = res.Content.ReadAsAsync<QualityMusicView>().Result;
            return View(model);
        }
        #endregion

        #region Order
        public ActionResult Order()
        {
            var res = APIService.client.GetAsync("Order").Result;
            var ls = res.Content.ReadAsAsync<IEnumerable<OrderView>>().Result;
            return View(ls);
        }
        #endregion
    }
}