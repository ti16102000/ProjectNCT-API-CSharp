using API.Models.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.DAO
{
    public class SingerMusicDAO
    {
        public static bool CreateSM(SingerMusic sm)
        {
            var en = new ProjectNCTEntities();
            en.SingerMusics.Add(sm);
            return en.SaveChanges() > 0 ? true : false;
        }
        public static IEnumerable<SingerMusic> GetSMByID(int id)
        {
            var en = new ProjectNCTEntities();
            return en.SingerMusics.Where(w => w.MusicID == id).ToList() ?? null;
        }
        public static bool DelSM(int id)
        {
            var en = new ProjectNCTEntities();
            en.SingerMusics.Remove(en.SingerMusics.Find(id));
            return en.SaveChanges() > 0 ? true : false;
        }
        public static bool DelSinger(int id, int idSinger)
        {
            var en = new ProjectNCTEntities();
            var item = en.SingerMusics.SingleOrDefault(s => s.MusicID == id && s.SingerID == idSinger);
            en.SingerMusics.Remove(item);
            return en.SaveChanges() > 0 ? true : false;
        }
    }
}