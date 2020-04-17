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
    public class PlaylistMusicController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var ls = Repositories.GetListPMByID(id).Select(s => new PlaylistMusicView { ID = s.ID, MusicDownloadAllowed = s.Music.MusicDownloadAllowed, MusicID = s.MusicID, MusicName = s.Music.MusicName, MusicView = s.Music.MusicView, PlaylistID = s.PlaylistID });
            return Ok(ls);
        }

        // POST api/<controller>
        public IHttpActionResult Post(PlaylistMusicView s)
        {
            var item= new PlaylistMusic { ID = s.ID, MusicID = s.MusicID, PlaylistID = s.PlaylistID };
            var res = Repositories.CreatePM(item);
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
            var res = Repositories.DelPM(id);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}