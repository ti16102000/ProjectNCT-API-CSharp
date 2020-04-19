using API.Models;
using API.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class NewFileController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var ls = Repositories.GetListNewFileMusic().Select(s => new QualityMusicView
            {
                ID = s.ID,
                MusicFile = s.MusicFile,
                MusicID = s.MusicID,
                NewFile = s.NewFile,
                QMusicApproved = s.QMusicApproved,
                ItemMusic = new MusicView {
                    MusicDayCreate=Repositories.GetMusicByID(s.MusicID).MusicDayCreate,
                    MusicImage= Repositories.GetMusicByID(s.MusicID).MusicImage,
                    MusicName= Repositories.GetMusicByID(s.MusicID).MusicName,
                    SongOrMV= Repositories.GetMusicByID(s.MusicID).SongOrMV,
                    UserID= Repositories.GetMusicByID(s.MusicID).UserID,
                    UserName= Repositories.GetMusicByID(s.MusicID).User.UserName,
                    //ListSinger= Repositories.GetSMByID(s.MusicID).Select(d=>new SingerMusicView { SingerID=d.SingerID,SingerName=d.User.UserName})
                }
            });
            return Ok(ls);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var res = Repositories.ApproveFile(id);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var res = Repositories.DelFileAndTableRelated(id);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}