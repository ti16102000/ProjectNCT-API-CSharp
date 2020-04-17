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
    public class SearchMenuController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get(string value,bool music) //search menu music
        {
            var ls=Repositories.GetListMusicSearch(value,music).Take(3).Select(s => new MusicView
            {
                CateID = s.CateID,
                ID = s.ID,
                MusicDayCreate = s.MusicDayCreate,
                MusicDownloadAllowed = s.MusicDownloadAllowed,
                MusicImage = s.MusicImage,
                MusicName = s.MusicName,
                MusicNameUnsigned = s.MusicNameUnsigned,
                MusicRelated = s.MusicRelated,
                SongOrMV = s.SongOrMV,
                UserID = s.UserID,
                View = s.MusicView,
                ListSinger = Repositories.GetSMByID(s.ID).Select(s1 => new SingerMusicView { ID = s1.ID, MusicID = s1.MusicID, SingerID = s1.SingerID, SingerName = s1.User.UserName })
            });
            return Ok(ls);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(string value) //search menu singer
        {
            var ls=Repositories.GetListSingerSearch(value).Take(3).Select(u => new UserView { ID = u.ID, DayVipEnd = u.DayVipEnd, TokenUser = u.TokenUser, RoleID = u.RoleID, UserActive = u.UserActive, UserDayCreate = u.UserDayCreate, UserDescription = u.UserDescription, UserDOB = u.UserDOB, UserEmail = u.UserEmail, UserGender = u.UserGender, UserImage = u.UserImage, UserName = u.UserName, UserNameUnsigned = u.UserNameUnsigned, UserVIP = u.UserVIP });
            return Ok(ls);
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