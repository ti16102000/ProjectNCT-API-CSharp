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
    public class PaymentController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var ls = Repositories.GetListPayment().Select(s => new PaymentView { PaymentID = s.ID, PaymentName = s.PaymentName });
            return Ok(ls);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public IHttpActionResult Post(PaymentView s)
        {
            var item = new Payment { ID = s.PaymentID, PaymentName = s.PaymentName };
            var res = Repositories.CreatePayment(item);
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
            var res = Repositories.DelPayment(id);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}