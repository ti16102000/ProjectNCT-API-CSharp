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
    public class CategoryController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            //var list = Repositories.GetListSuperCate().Select(s => new CategoryView
            //{
            //    ID = s.ID,
            //    CateName = s.CateName
            //    ListSubCate = Repositories.GetListSubCateByID(s.ID).Select(s1 => new CategoryView { ID = s1.ID, CateName = s1.CateName }).ToList()
            //});
            //return Ok(list);
            var list = new List<CategoryView>();
            var dataList = Repositories.GetListSuperCate();
            foreach (var item in dataList)
            {
                var newCate = new CategoryView
                {
                    ID = item.ID,
                    CateName = item.CateName,
                    ListSubCate = Repositories.GetListSubCateByID(item.ID).Select(s => new CategoryView { ID = s.ID, CateName = s.CateName }).ToList()
                };
                list.Add(newCate);
            }
            return Ok(list);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var item = Repositories.GetCateByID(id);
            var cate = new CategoryView { ID = item.ID, CateName = item.CateName, ID_root = item.ID_root };
            if (item != null)
            {
                return Ok(cate);
            }
            return InternalServerError();
        }
        public IHttpActionResult GetSubCate(int idroot)
        {
            var list = Repositories.GetListSubCateByID(idroot).Select(s => new CategoryView { ID = s.ID, CateName = s.CateName, ID_root = s.ID_root });
            return Ok(list);
        }
        // POST api/<controller>
        public IHttpActionResult Post(CategoryView c)
        {
            var newCate = new Category { ID = c.ID, CateName = c.CateName, ID_root = c.ID_root };
            var res = Repositories.CreateCate(newCate);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, Category c)
        {
            var cate = new Category { ID = c.ID, CateName = c.CateName, ID_root = c.ID_root };
            var res = Repositories.UpdateCate(cate);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var res = Repositories.DelCate(id);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}