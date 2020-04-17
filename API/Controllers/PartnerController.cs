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
    public class PartnerController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var ls = Repositories.GetListPartner().Select(s => new PartnerView { ID = s.ID, PartnerActive = s.PartnerActive, PartnerDayCreate = s.PartnerDayCreate, PartnerLink = s.PartnerLink, PartnerImage = s.PartnerImage, PartnerName = s.PartnerName });
            return Ok(ls);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var s = Repositories.GetPartnerByID(id);
            var item = new PartnerView { ID = s.ID, PartnerActive = s.PartnerActive, PartnerDayCreate = s.PartnerDayCreate, PartnerLink = s.PartnerLink, PartnerImage = s.PartnerImage, PartnerName = s.PartnerName };
            return Ok(item);
        }

        // POST api/<controller>
        public IHttpActionResult Post(PartnerView s)
        {
            var item = new Partner { ID = s.ID, PartnerActive = s.PartnerActive, PartnerDayCreate = s.PartnerDayCreate, PartnerLink = s.PartnerLink, PartnerImage = s.PartnerImage, PartnerName = s.PartnerName };
            var res = Repositories.CreatePartner(item);
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
            var res = Repositories.DelPartner(id);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}