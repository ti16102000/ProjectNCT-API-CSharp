﻿using API.Models;
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
        public IHttpActionResult GetSearch(string value)
        {
            var ls = Repositories.GetListPlaylistSearch(value).Select(p => new PlaylistView { ID = p.ID, CateID = p.CateID, CateName = p.Category.CateName, PlaylistDescription = p.PlaylistDescription, PlaylistImage = p.PlaylistImage, PlaylistName = p.PlaylistName,UserID=p.UserID });
            return Ok(ls);
        }
        // GET api/<controller>/5
        public IHttpActionResult GetID(int id)
        {
            var p = Repositories.GetPlaylistByID(id);
            var item = new PlaylistView { ID = p.ID, CateID = p.CateID, CateName = p.Category.CateName, PlaylistDescription = p.PlaylistDescription, PlaylistImage = p.PlaylistImage, PlaylistName = p.PlaylistName,UserID=p.UserID };
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
        public IHttpActionResult Put(int id, PlaylistView p)
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
        public void Delete(int id)
        {
        }
    }
}