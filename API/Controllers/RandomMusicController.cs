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
    public class RandomMusicController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int idCate,bool music)
        {
            var ls=Repositories.GetListMusicByIDCate(idCate,music).OrderBy(o=>Guid.NewGuid()).Take(5).Select(s => new MusicView
            {
                CateID = s.CateID,
                ID = s.ID,
                MusicDayCreate = s.MusicDayCreate,
                MusicDownloadAllowed = s.MusicDownloadAllowed,
                MusicImage = s.MusicImage,
                MusicName = s.MusicName,
                MusicNameUnsigned = s.MusicNameUnsigned,
                MusicRelated = s.MusicRelated,
                SongOrMV = s.SongOrMV,
                UserID = s.UserID,
                View = s.MusicView,
                ListSinger = Repositories.GetSMByID(s.ID).Select(s1 => new SingerMusicView { ID = s1.ID, MusicID = s1.MusicID, SingerID = s1.SingerID, SingerName = s1.User.UserName })
            });
            return Ok(ls);
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
        public void Delete(int id)
        {
        }
    }
}