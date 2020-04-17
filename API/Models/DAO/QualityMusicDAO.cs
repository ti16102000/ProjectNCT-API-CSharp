using API.Models.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.DAO
{
    public class QualityMusicDAO
    {
        public static bool CreateQM(QualityMusic qm)
        {
            var en = new ProjectNCTEntities();
            en.QualityMusics.Add(qm);
            return en.SaveChanges() > 0 ? true : false;
        }
        public static QualityMusic GetQMByID(int id)
        {
            var en = new ProjectNCTEntities();
            return en.QualityMusics.SingleOrDefault(w => w.ID==id) ?? null;
        }
        public static QualityMusic GetQMByIDMusic(int id,bool vip)
        {
            var en = new ProjectNCTEntities();
            return en.QualityMusics.SingleOrDefault(w => w.MusicID == id && w.Quality.QualityVip == vip)??null;
        }
        public static IEnumerable<QualityMusic> GetListQM(int id)
        {
            var en = new ProjectNCTEntities();
            return en.QualityMusics.Where(w => w.MusicID == id).ToList()??null;
        }
        public static bool DelQM(int id)
        {
            var en = new ProjectNCTEntities();
            en.QualityMusics.Remove(en.QualityMusics.Find(id));
            return en.SaveChanges() > 0 ? true : false;
        }
    }
}