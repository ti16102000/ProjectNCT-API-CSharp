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
    public class LoginController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(string email,string pwd)
        {
            var u = Repositories.LoginNormal(email, pwd);
            if (u != null)
            {
                var item = new UserView { ID = u.ID, UserName = u.UserName, DayVipEnd = u.DayVipEnd, RoleID = u.RoleID, TokenUser = u.TokenUser, UserActive = u.UserActive, UserDayCreate = u.UserDayCreate, UserDescription = u.UserDescription, UserDOB = u.UserDOB, UserEmail = u.UserEmail, UserGender = u.UserGender, UserImage = u.UserImage, UserNameUnsigned = u.UserNameUnsigned, UserPwd = u.UserPwd, UserVIP = u.UserVIP };
                return Ok(item);
            }
            return InternalServerError();
        }
        public IHttpActionResult GetChangepass(string mail,string pwdNew)
        {
            var item = Repositories.ChangePwd(mail,pwdNew);
            if (item > 0)
            {
                return Ok(item);
            }
            return InternalServerError();
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