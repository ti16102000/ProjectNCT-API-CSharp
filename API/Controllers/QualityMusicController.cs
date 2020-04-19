using API.Models;
using API.Models.ModelEntities;
using API.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class QualityMusicController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult GetList(int idMusic)
        {
            var ls = Repositories.GetListQM(idMusic).Select(q => new QualityMusicView { ID = q.ID, MusicFile = q.MusicFile, MusicID = q.MusicID, NewFile = q.NewFile, QMusicApproved = q.QMusicApproved, QualityID = q.QualityID, QualityName = q.Quality.QualityName, QualityVip = q.Quality.QualityVip });
            return Ok(ls);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var q = Repositories.GetQMByID(id);
            var item = new QualityMusicView { ID = q.ID, MusicFile = q.MusicFile, MusicID = q.MusicID, NewFile = q.NewFile, QMusicApproved = q.QMusicApproved, QualityID = q.QualityID, QualityName = q.Quality.QualityName, QualityVip = q.Quality.QualityVip,ItemMusic=new MusicView { MusicImage=Repositories.GetMusicByID(q.MusicID).MusicImage} };
            return Ok(item);
        }
        public IHttpActionResult getqm(int idMusic, bool vip)
        {
            var q = Repositories.GetQMByIDMusic(idMusic, vip);
            var item = new QualityMusicView {
                ID = q.ID,
                MusicFile = q.MusicFile,
                MusicID = q.MusicID,
                NewFile = q.NewFile,
                QMusicApproved = q.QMusicApproved,
                QualityID = q.QualityID,
                QualityName = q.Quality.QualityName,
                QualityVip = q.Quality.QualityVip,
                ItemMusic=new MusicView {
                    MusicDayCreate = Repositories.GetMusicByID(q.MusicID).MusicDayCreate,
                    MusicImage = Repositories.GetMusicByID(q.MusicID).MusicImage,
                    MusicName = Repositories.GetMusicByID(q.MusicID).MusicName,
                    SongOrMV = Repositories.GetMusicByID(q.MusicID).SongOrMV,
                    UserID = Repositories.GetMusicByID(q.MusicID).UserID,
                    UserName = Repositories.GetMusicByID(q.MusicID).User.UserName,
                    CateName=Repositories.GetCateByID(Repositories.GetMusicByID(q.MusicID).CateID).CateName,
                    ListSinger = Repositories.GetSMByID(q.MusicID).Select(d => new SingerMusicView { SingerID = d.SingerID, SingerName = d.User.UserName })
                }
            };
            return Ok(item);
        }
        // POST api/<controller>
        public IHttpActionResult Post(QualityMusicView q)
        {
            var item= new QualityMusic {MusicFile = q.MusicFile, MusicID = q.MusicID, NewFile = q.NewFile, QMusicApproved = q.QMusicApproved, QualityID = q.QualityID };
            var res = Repositories.CreateQM(item);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var res = Repositories.DelQM(id);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}