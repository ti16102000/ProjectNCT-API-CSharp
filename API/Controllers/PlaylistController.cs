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
    public class PlaylistController : ApiController
    {
        // GET api/<controller>
        public IHttpActionResult Get(int idUser)
        {
            var ls = new List<PlaylistView>();
            var datals = Repositories.GetListPlaylistByIDUser(idUser);
            foreach (var item in datals)
            {
                ls.Add(new PlaylistView { ID = item.ID, CateID = item.CateID, PlaylistDescription = item.PlaylistDescription, PlaylistImage = item.PlaylistImage, PlaylistName = item.PlaylistName, UserID = item.UserID });
            }
            return Ok(ls);
        }
        public IHttpActionResult GetPL(int idCate)
        {
            var ls = Repositories.GetListPlaylistByIDCate(idCate).Select(p => new PlaylistView { ID = p.ID, CateID = p.CateID, CateName = p.Category.CateName, PlaylistDescription = p.PlaylistDescription, PlaylistImage = p.PlaylistImage, PlaylistName = p.PlaylistName,UserID=p.UserID,UserName=Repositories.GetUserByID(p.UserID).UserName });
            return Ok(ls);
        }
        public IHttpActionResult GetPlist(int idCate,int number)
        {
            var list = new List<PlaylistView>();
            var ls = Repositories.GetListPlaylistByIDCate(idCate).OrderBy(o => Guid.NewGuid()).Take(number).ToList();
            foreach (var item in ls)
            {
                list.Add(new PlaylistView {
                    ID = item.ID,
                    PlaylistDescription = item.PlaylistDescription,
                    PlaylistImage = item.PlaylistImage,
                    PlaylistName = item.PlaylistName,
                    UserID = item.UserID,
                    CateID = item.CateID,
                    CateName = item.Category.CateName,
                    UserName = Repositories.GetUserByID(item.UserID).UserName
                });
            }
            return Ok(list);
        }
        public IHttpActionResult GetSearch(string value)
        {
            var list = new List<PlaylistView>();
            var ls = Repositories.GetListPlaylistSearch(value);
            foreach (var i in ls)
            {
                list.Add(new PlaylistView { ID = i.ID,PlaylistImage = i.PlaylistImage, PlaylistName = i.PlaylistName, UserID = i.UserID,UserName=Repositories.GetUserByID(i.UserID).UserName});
            }
            return Ok(list);
        }
        // GET api/<controller>/5
        public IHttpActionResult GetID(int id)
        {
            var p = Repositories.GetPlaylistByID(id);
            var item = new PlaylistView { ID = p.ID, CateID = p.CateID, CateName = p.Category.CateName, PlaylistDescription = p.PlaylistDescription, PlaylistImage = p.PlaylistImage, PlaylistName = p.PlaylistName,UserID=p.UserID,UserName=Repositories.GetUserByID(p.UserID).UserName,UserImg=Repositories.GetUserByID(p.UserID).UserImage };
            return Ok(item);
        }

        // POST api/<controller>
        public IHttpActionResult Post(PlaylistView p)
        {
            var item = new Playlist { ID = p.ID, CateID = p.CateID, PlaylistDescription = p.PlaylistDescription, PlaylistImage = p.PlaylistImage, PlaylistName = p.PlaylistName,UserID=p.UserID };
            var res = Repositories.CreatePlaylist(item);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }

        // PUT api/<controller>/5
        public IHttpActionResult Put(int id,[FromBody] PlaylistView p)
        {
            var item = new Playlist { ID = p.ID, CateID = p.CateID, PlaylistDescription = p.PlaylistDescription, PlaylistImage = p.PlaylistImage, PlaylistName = p.PlaylistName,UserID=p.UserID };
            var res = Repositories.UpdatePlaylist(item);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }

        // DELETE api/<controller>/5
        public IHttpActionResult Delete(int id)
        {
            var res = Repositories.DelPlaylist(id);
            if (res == true)
            {
                return Ok();
            }
            return InternalServerError();
        }
    }
}