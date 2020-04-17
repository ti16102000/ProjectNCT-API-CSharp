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
    public class HistoryUserController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int idUser,bool music)
        {
            var list = new List<MusicView>();
            var ls = Repositories.GetListMusicHUByIDUser(idUser, music);
            foreach (var item in ls)
            {
                list.Add(new MusicView
                {
                    ID = item.MusicID,
                    MusicName = item.Music.MusicName,
                    MusicImage = item.Music.MusicImage,
                    ListSinger = Repositories.GetSMByID(item.MusicID).Select(s => new SingerMusicView { SingerID = s.SingerID, SingerName = s.User.UserName })
                });
            }
            return Ok(list);
        }
        public IHttpActionResult DelMusic(int idUsr,int idMusic)
        {
            var res = Repositories.DelMusicHU(idUsr, idMusic);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }
        public IHttpActionResult DelListMusic(int idUsr, bool music)
        {
            var res = Repositories.DelListMusicHU(idUsr, music);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }
        // POST api/<controller>
        public IHttpActionResult Post(HistoryUserView hu)
        {
            var item = new HistoryUser { UserID = hu.UserID, MusicID = hu.MusicID };
            var res = Repositories.CreateHU(item);
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
        public void Delete(int id)
        {
        }
    }
}