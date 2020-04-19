using API.Models.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.DAO
{
    public class PlaylistDAO
    {
        public static bool CreatePlaylist(Playlist pl)
        {
            var en = new ProjectNCTEntities();
            en.Playlists.Add(pl);
            return en.SaveChanges() > 0 ? true : false;
        }
        public static Playlist GetPlaylistByID(int id)
        {
            var en = new ProjectNCTEntities();
            return en.Playlists.Find(id) ?? null;
        }
        public static IEnumerable<Playlist> GetListPlaylistByIDCate(int id)
        {
            var en = new ProjectNCTEntities();
            return en.Playlists.Where(w=>w.CateID==id).ToList() ?? null;
        }
        public static IEnumerable<Playlist> GetListPlaylistByIDUser(int id)
        {
            var en = new ProjectNCTEntities();
            return en.Playlists.Where(w => w.UserID == id && w.PlaylistName!=null).ToList() ?? null;
        }
        public static IEnumerable<Playlist> Get3PlaylistByIDCate(int id)
        {
            var en = new ProjectNCTEntities();
            return en.Playlists.Where(w => w.UserID == id).OrderBy(s => Guid.NewGuid()).Take(3).ToList() ?? null;
        }
        public static bool UpdatePlaylist(Playlist pl)
        {
            var en = new ProjectNCTEntities();
            var item = en.Playlists.SingleOrDefault(s => s.ID == pl.ID);
            item.PlaylistName = pl.PlaylistName;
            item.PlaylistImage = pl.PlaylistImage;
            item.PlaylistDescription = pl.PlaylistDescription;
            item.CateID = pl.CateID;
            return en.SaveChanges() > 0 ? true : false;
        }
        public static IEnumerable<Playlist> GetListPlaylistSearch(string value)
        {
            var en = new ProjectNCTEntities();
            return en.Playlists.Where(w => w.PlaylistName.ToLower().Contains(value.ToLower())).ToList()??null;
        }
        public static bool DelPlaylist(int id)
        {
            var en = new ProjectNCTEntities();
            var item = en.Playlists.SingleOrDefault(s => s.ID == id);
            var ls = PlaylistMusicDAO.GetListPM(id);
            if (ls != null)
            {
                foreach (var pm in ls)
                {
                    en.PlaylistMusics.Remove(en.PlaylistMusics.Find(pm.ID));
                }
            }
            en.Playlists.Remove(item);
            return en.SaveChanges() > 0 ? true : false;
        }
    }
}