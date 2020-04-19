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
    public class OrderController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var ls = Repositories.GetListOrd().Select(s => new OrderView
            {
                ID=s.ID,
                PaymentID=s.PaymentID,
                PaymentName=s.Payment.PaymentName,
                PVipID=s.PVipID,
                PVipName=s.PackageVip.PVipName,
                OrdPrice=s.OrdPrice,
                OrdDayCreate=s.OrdDayCreate,
                UserID=s.UserID,
                UserName=s.User.UserName
            });
            return Ok(ls);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var ls = Repositories.GetListOrdByIDUser(id).Select(s => new OrderView
            {
                ID = s.ID,
                PaymentID = s.PaymentID,
                PaymentName = s.Payment.PaymentName,
                PVipID = s.PVipID,
                PVipName = s.PackageVip.PVipName,
                OrdPrice = s.OrdPrice,
                OrdDayCreate = s.OrdDayCreate,
                UserID = s.UserID,
                UserName = s.User.UserName
            });
            return Ok(ls);
        }

        // POST api/<controller>
        public IHttpActionResult Post(OrderView o)
        {
            var item = new OrderVip { UserID = o.UserID, OrdPrice = o.OrdPrice, PVipID = o.PVipID, PaymentID = o.PaymentID };
            var res = Repositories.CreateOrd(item);
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