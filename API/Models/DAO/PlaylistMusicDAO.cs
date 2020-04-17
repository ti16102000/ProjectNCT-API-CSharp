using API.Models.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.DAO
{
    public class PlaylistMusicDAO
    {
        public static bool CreatePM(PlaylistMusic pm)
        {
            var en = new ProjectNCTEntities();
            en.PlaylistMusics.Add(pm);
            return en.SaveChanges() > 0 ? true : false;
        }
        public static IEnumerable<PlaylistMusic> GetListPM(int id)
        {
            var en = new ProjectNCTEntities();
            return en.PlaylistMusics.Where(w => w.PlaylistID == id).ToList() ?? null;
        }
        public static bool DelPM(int id)
        {
            var en = new ProjectNCTEntities();
            en.PlaylistMusics.Remove(en.PlaylistMusics.Find(id));
            return en.SaveChanges() > 0 ? true : false;
        }
    }
}