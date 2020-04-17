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
    public class QualityController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var ls = Repositories.GetListQuality().Select(s => new QualityMusicView { QualityID = s.ID, QualityName = s.QualityName, QualityVip = s.QualityVip });
            return Ok(ls);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var item = Repositories.GetQualityByID(id);
            var q = new QualityMusicView { QualityID = item.ID, QualityName = item.QualityName, QualityVip = item.QualityVip };
            return Ok(q);
        }

        // POST api/<controller>
        public IHttpActionResult Post(QualityMusicView qv)
        {
            var q = new Quality { ID = qv.QualityID, QualityName = qv.QualityName, QualityVip = qv.QualityVip };
            var res = Repositories.CreateQuality(q);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, QualityMusicView qv)
        {
            var q = new Quality { ID = qv.QualityID, QualityName = qv.QualityName, QualityVip = qv.QualityVip };
            var res = Repositories.UpdateQuality(q);
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