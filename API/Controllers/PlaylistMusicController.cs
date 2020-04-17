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
        public IHttpActionResult GetList(int idPlist)
        {
            var ls = Repositories.GetListPMByID(idPlist).Select(s => new MusicView
            {
                ID = s.MusicID,
                MusicDownloadAllowed = s.Music.MusicDownloadAllowed,
                CateID = s.Music.CateID,
                MusicImage = s.Music.MusicImage,
                MusicName = s.Music.MusicName,
                SongOrMV = s.Music.SongOrMV,
                View = s.Music.MusicView,
                FileNormal = Repositories.GetQMByIDMusic(s.MusicID, false).MusicFile,
                ListSinger = Repositories.GetSMByID(s.MusicID).Select(d => new SingerMusicView { SingerID = d.SingerID, SingerName = d.User.UserName })
            });
            return Ok(ls);
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