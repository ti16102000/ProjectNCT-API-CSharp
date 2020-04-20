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
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/<controller>/5
        public IHttpActionResult Get(int idUser,bool music)
        {
            var list = new List<HistoryUserView>();
            var ls = Repositories.GetListMusicHUByIDUser(idUser, music).GroupBy(g=>g.MusicID).Select(s=>s.First()).OrderByDescending(o=>o.ID) ;
            foreach (var item in ls)
            {
                list.Add(new HistoryUserView {
                    ID=item.ID,
                    UserID=item.UserID,
                    MusicID=item.MusicID,
                    ItemMusic=new MusicView {
                        MusicName=item.Music.MusicName,
                        MusicImage=item.Music.MusicImage,
                        ListSinger=Repositories.GetSMByID(item.MusicID).Select(s=>new SingerMusicView { SingerID=s.SingerID,SingerName=s.User.UserName})
                    }
                });
            }
            return Ok(list);
        }
        public IHttpActionResult GetListDel(int idUsr, bool music)
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
        public IHttpActionResult Delete(int id)
        {
            var res = Repositories.DelMusicHU(id);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}