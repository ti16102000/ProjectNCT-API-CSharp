using API.Models;
using API.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    public class SubCateController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var list = Repositories.GetListSubCate().Select(s => new CategoryView { ID = s.ID, CateName = s.CateName, ID_root = s.ID_root });
            return Ok(list);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var list = Repositories.GetListSubCate().OrderBy(o=>Guid.NewGuid()).Take(id).Select(s => new CategoryView { ID = s.ID, CateName = s.CateName, ID_root = s.ID_root });
            return Ok(list);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
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