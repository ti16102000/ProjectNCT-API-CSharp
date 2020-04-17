using API.Models.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.DAO
{
    public class LyricsMusicDAO
    {
        public static bool CreateLM(LyricsMusic lm)
        {
            lm.LMusicDayCreate = DateTime.Now;
            var en = new ProjectNCTEntities();
            en.LyricsMusics.Add(lm);
            return en.SaveChanges() > 0 ? true : false;
        }
        public static LyricsMusic GetLMByID(int id)
        {
            var en = new ProjectNCTEntities();
            return en.LyricsMusics.SingleOrDefault(w => w.MusicID == id) ?? null;
        }
        public static bool UpdateLM(LyricsMusic lm)
        {
            var en = new ProjectNCTEntities();
            var item = en.LyricsMusics.SingleOrDefault(s => s.ID == lm.ID);
            item.LMusicDetail = lm.LMusicDetail;
            return en.SaveChanges() > 0 ? true : false;
        }
    }
}