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
    public class SingerMusicController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var ls = Repositories.GetSMByID(id).Select(s => new SingerMusicView { ID = s.ID, MusicID = s.MusicID, SingerID = s.SingerID,SingerName=s.User.UserName });
            return Ok(ls);
        }
        //public IHttpActionResult DelSinger(int idMusic,int idSinger)
        //{
        //    var res = Repositories.DelSinger(idMusic, idSinger);
        //    if (res == true)
        //    {
        //        return Ok();
        //    }
        //    return InternalServerError();
        //}
        // POST api/<controller>
        public IHttpActionResult Post(SingerMusicView s)
        {
            var sm = new SingerMusic { ID = s.ID, MusicID = s.MusicID, SingerID = s.SingerID };
            var res = Repositories.CreateSM(sm);
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
            var res = Repositories.DelSM(id);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}