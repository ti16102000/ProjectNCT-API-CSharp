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
    public class LyricsController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var u = Repositories.GetLMByID(id);
            var item = new LyricsView { ID = u.ID, LMusicDayCreate = u.LMusicDayCreate, LMusicDetail = u.LMusicDetail, MusicID = u.MusicID, UserID = u.UserID, UserName = Repositories.GetUserByID(u.UserID).UserName };
            return Ok(item);
        }

        // POST api/<controller>
        public IHttpActionResult Post(LyricsView lv)
        {
            var item = new LyricsMusic { ID = lv.ID, LMusicDayCreate = lv.LMusicDayCreate, LMusicDetail = lv.LMusicDetail, MusicID = lv.MusicID, UserID = lv.UserID };
            var res = Repositories.CreateLM(item);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, LyricsView lv)
        {
            var item = new LyricsMusic { ID = lv.ID, LMusicDayCreate = lv.LMusicDayCreate, LMusicDetail = lv.LMusicDetail, MusicID = lv.MusicID, UserID = lv.UserID };
            var res = Repositories.UpdateLM(item);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}