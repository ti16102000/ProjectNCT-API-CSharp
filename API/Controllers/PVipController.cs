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
    public class PVipController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var ls = Repositories.GetListPV().Select(s => new PackageVipView { PVipID = s.ID, PVipName = s.PVipName, PVipMonths = s.PVipMonths, PVipPrice = s.PVipPrice });
            return Ok(ls);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var p = Repositories.GetPVipByID(id);
            var item = new PackageVipView { PVipID = p.ID, PVipMonths = p.PVipMonths, PVipName = p.PVipName, PVipPrice = p.PVipPrice };
            return Ok(item);
        }

        // POST api/<controller>
        public IHttpActionResult Post(PackageVipView s)
        {
            var item = new PackageVip { ID = s.PVipID, PVipName = s.PVipName, PVipMonths = s.PVipMonths, PVipPrice = s.PVipPrice };
            var res = Repositories.CreatePV(item);
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
            var res = Repositories.DelPV(id);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}