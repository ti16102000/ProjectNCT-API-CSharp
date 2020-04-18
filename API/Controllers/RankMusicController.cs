using API.Models;
using API.Models.ModelEntities;
using API.Models.ModelViews;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace API.Controllers
{
    public class RankMusicController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult GetList(bool musicRank)
        {
            var ls = Repositories.GetListRank(musicRank).Select(s => new RankView { ID = s.ID,CateName=s.Category.CateName ,CateID = s.CateID, RMusicEnd = s.RMusicEnd, RMusicName = s.RMusicName, RMusicStart = s.RMusicStart, SongOrMusic = s.SongOrMusic });
            return Ok(ls);
        }
        public IHttpActionResult GetListRM(int idRank)
        {
            var ls = Repositories.GetListRM(idRank).Select(s => new RankMusicView {
                ID = s.ID,
                RMusicGrade = s.RMusicGrade,
                ItemMusic = new MusicView
                {
                    ID = s.MusicID,
                    MusicImage = s.Music.MusicImage,
                    MusicName = s.Music.MusicName,
                    View = s.Music.MusicView,
                    ListSinger = Repositories.GetSMByID(s.MusicID).Select(d => new SingerMusicView { SingerID = d.SingerID, SingerName = d.User.UserName })
                }
            });
            return Ok(ls);
        }
        public IHttpActionResult GetRank(int id)
        {
            var r = Repositories.GetRankByID(id);
            RankView item = new RankView
            {
                ID = r.ID,
                CateID = r.CateID,
                RMusicEnd = r.RMusicEnd,
                RMusicName = r.RMusicName,
                RMusicStart = r.RMusicStart,
                SongOrMusic = r.SongOrMusic
            };
            return Ok(item);
        }
        // GET api/<controller>/5
        public IHttpActionResult Get(int idCate,bool music)
        {
            Rank r = Repositories.GetRankNewest(idCate,music);
            RankView item = new RankView
            {
                ID = r.ID,
                CateID = r.CateID,
                RMusicEnd = r.RMusicEnd,
                RMusicName = r.RMusicName,
                RMusicStart = r.RMusicStart,
                SongOrMusic = r.SongOrMusic,
            };
            return Ok(item);
        }

    // POST api/<controller>
    public IHttpActionResult Post(RankView r)
    {
        Rank item = new Rank { CateID = r.CateID, RMusicEnd = r.RMusicEnd, RMusicName = r.RMusicName, RMusicStart = r.RMusicStart, SongOrMusic = r.SongOrMusic };
        bool res = Repositories.CreateRank(item);
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
        bool res = Repositories.DelRank(id);
        if (res == true)
        {
            return Ok();
        }
        return InternalServerError();
    }
}
}