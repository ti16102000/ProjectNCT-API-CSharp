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
        public static IEnumerable<QualityMusic> GetListNewFileMusic()
        {
            var en = new ProjectNCTEntities();
            return en.QualityMusics.Where(w => w.NewFile == true).ToList();
        }
        public static bool ApproveFile(int id)
        {
            var en = new ProjectNCTEntities();
            var item = en.QualityMusics.Find(id);
            item.NewFile = false;
            item.QMusicApproved = true;
            return en.SaveChanges() > 0 ? true : false;
        }
        public static bool DelQM(int id)
        {
            var en = new ProjectNCTEntities();
            en.QualityMusics.Remove(en.QualityMusics.Find(id));
            return en.SaveChanges() > 0 ? true : false;
        }
        public static bool DelFileAndTableRelated(int id)
        {
            var en = new ProjectNCTEntities();
            var item = en.QualityMusics.Find(id);
            var lsSinger = SingerMusicDAO.GetSMByID(item.MusicID);
            foreach (var singer in lsSinger)
            {
                en.SingerMusics.Remove(en.SingerMusics.Find(singer.ID));
                en.SaveChanges();
            }
            DelQM(id);
            return MusicDAO.DelMusic(item.MusicID) == true ? true : false;
        }
    }
}