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
    public class UserController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get()
        {

            var ls = Repositories.GetListSinger().Select(u => new UserView { ID = u.ID, DayVipEnd = u.DayVipEnd, TokenUser = u.TokenUser, RoleID = u.RoleID, UserActive = u.UserActive, UserDayCreate = u.UserDayCreate, UserDescription = u.UserDescription, UserDOB = u.UserDOB, UserEmail = u.UserEmail, UserGender = u.UserGender, UserImage = u.UserImage, UserName = u.UserName, UserNameUnsigned = u.UserNameUnsigned, UserVIP = u.UserVIP });
            return Ok(ls);
        }
        public IHttpActionResult Getvip(bool vip)
        {

            var ls = Repositories.GetListUser(vip).Select(u => new UserView { ID = u.ID, DayVipEnd = u.DayVipEnd, TokenUser = u.TokenUser, RoleID = u.RoleID, UserActive = u.UserActive, UserDayCreate = u.UserDayCreate, UserDescription = u.UserDescription, UserDOB = u.UserDOB, UserEmail = u.UserEmail, UserGender = u.UserGender, UserImage = u.UserImage, UserName = u.UserName, UserNameUnsigned = u.UserNameUnsigned, UserVIP = u.UserVIP });
            return Ok(ls);
        }
        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var u = Repositories.GetUserByID(id);
            var item = new UserView { ID = u.ID, DayVipEnd = u.DayVipEnd, TokenUser = u.TokenUser, RoleID = u.RoleID, UserActive = u.UserActive, UserDayCreate = u.UserDayCreate, UserDescription = u.UserDescription, UserDOB = u.UserDOB, UserEmail = u.UserEmail, UserGender = u.UserGender, UserImage = u.UserImage, UserName = u.UserName, UserNameUnsigned = u.UserNameUnsigned, UserVIP = u.UserVIP,UserPwd=u.UserPwd };
            return Ok(item);
        }

        // POST api/<controller>
        public IHttpActionResult Post(UserView u)
        {
            var item = new User { ID = u.ID,UserPwd=u.UserPwd, DayVipEnd = u.DayVipEnd, TokenUser = u.TokenUser, RoleID = u.RoleID, UserActive = u.UserActive, UserDayCreate = u.UserDayCreate, UserDescription = u.UserDescription, UserDOB = u.UserDOB, UserEmail = u.UserEmail, UserGender = u.UserGender, UserImage = u.UserImage, UserName = u.UserName, UserNameUnsigned = u.UserNameUnsigned, UserVIP = u.UserVIP };
            var res = Repositories.CreateUser(item);
            if (res >0)
            {
                return Ok(res);
            }
            return InternalServerError();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, UserView u)
        {
            var item = new User { ID = u.ID, DayVipEnd = u.DayVipEnd, TokenUser = u.TokenUser, RoleID = u.RoleID, UserActive = u.UserActive, UserDayCreate = u.UserDayCreate, UserDescription = u.UserDescription, UserDOB = u.UserDOB, UserEmail = u.UserEmail, UserGender = u.UserGender, UserImage = u.UserImage, UserName = u.UserName, UserNameUnsigned = u.UserNameUnsigned, UserVIP = u.UserVIP };
            var res = Repositories.UpdateUser(item);
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