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
    public class MusicController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get(int idSinger, bool music)
        {
            var ls = Repositories.GetListMusicForSinger(idSinger, music).Select(s => new MusicView { CateID = s.CateID, ID = s.ID, MusicDayCreate = s.MusicDayCreate, MusicDownloadAllowed = s.MusicDownloadAllowed, MusicImage = s.MusicImage, MusicName = s.MusicName, MusicNameUnsigned = s.MusicNameUnsigned, MusicRelated = s.MusicRelated, SongOrMV = s.SongOrMV, UserID = s.UserID,View=s.MusicView });
            return Ok(ls);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var s = Repositories.GetMusicByID(id);
            var item = new MusicView { CateID = s.CateID, ID = s.ID, MusicDayCreate = s.MusicDayCreate, UserName=s.User.UserName,UserImg=s.User.UserImage,MusicDownloadAllowed = s.MusicDownloadAllowed, MusicImage = s.MusicImage, MusicName = s.MusicName, MusicNameUnsigned = s.MusicNameUnsigned, MusicRelated = s.MusicRelated, SongOrMV = s.SongOrMV, UserID = s.UserID, View = s.MusicView,ListSinger=Repositories.GetSMByID(s.ID).Select(d=>new SingerMusicView { ID=d.ID,MusicID=d.MusicID,SingerID=d.SingerID,SingerName=d.User.UserName})};
            return Ok(item);
        }
        public IHttpActionResult GetView(int idMusic)
        {
            var view = Repositories.UpdateView(idMusic);
            if (view > 0)
            {
                return Ok(view);
            }
            return InternalServerError();
        }
        public IHttpActionResult GetList(int idCate,bool music)
        {
            var ls=Repositories.GetListMusicByIDCate(idCate,music).Select(s => new MusicView
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
        public IHttpActionResult GetMusic(bool music)
        {
            var ls = Repositories.GetListMusic(music).OrderByDescending(o=>o.MusicDayCreate).Take(10).Select(s => new MusicView {
                CateID = s.CateID,
                ID = s.ID,
                MusicDayCreate = s.MusicDayCreate,
                MusicDownloadAllowed = s.MusicDownloadAllowed,
                MusicImage = s.MusicImage, MusicName = s.MusicName,
                MusicNameUnsigned = s.MusicNameUnsigned,
                MusicRelated = s.MusicRelated,
                SongOrMV = s.SongOrMV,
                UserID = s.UserID,
                View = s.MusicView,
                ListSinger = Repositories.GetSMByID(s.ID).Select(s1 => new SingerMusicView { ID = s1.ID, MusicID = s1.MusicID, SingerID = s1.SingerID, SingerName = s1.User.UserName })
            });
            return Ok(ls);
        }
        public IHttpActionResult GetRElated(int idMusic, int idRelated)
        {
            var res = Repositories.UpdateRelated(idMusic, idRelated);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }
        public IHttpActionResult GetSearch(string value,bool music)
        {
            var ls=Repositories.GetListMusicSearch(value,music).Select(s => new MusicView
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
        // POST api/<controller>
        public IHttpActionResult Post(MusicView s)
        {
            var item = new Music { CateID = s.CateID, ID = s.ID, MusicDayCreate = s.MusicDayCreate, MusicDownloadAllowed = s.MusicDownloadAllowed, MusicImage = s.MusicImage, MusicName = s.MusicName, MusicNameUnsigned = s.MusicNameUnsigned, MusicRelated = s.MusicRelated, SongOrMV = s.SongOrMV, UserID = s.UserID,MusicView=s.View };
            var res = Repositories.CreateMusic(item);
            if (res >0)
            {
                return Ok(res);
            }
            return InternalServerError();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id, MusicView s)
        {
            var item = new Music { CateID = s.CateID, ID = s.ID, MusicDayCreate = s.MusicDayCreate, MusicDownloadAllowed = s.MusicDownloadAllowed, MusicImage = s.MusicImage, MusicName = s.MusicName, MusicNameUnsigned = s.MusicNameUnsigned, MusicRelated = s.MusicRelated, SongOrMV = s.SongOrMV, UserID = s.UserID, MusicView = s.View };
            var res = Repositories.UpdateMusic(item);
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