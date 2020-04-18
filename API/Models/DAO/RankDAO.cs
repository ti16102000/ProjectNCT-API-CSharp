using API.Models.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.DAO
{
    public class RankDAO
    {
        public static bool CreateRank(Rank r)
        {
            var en = new ProjectNCTEntities();
            var newest = GetRankNewest(r.CateID,r.SongOrMusic);
            if (newest == null || r.RMusicStart == newest.RMusicEnd)
            {
                en.Ranks.Add(r);
                en.SaveChanges();
                var id = r.ID;
                var grade = 1;
                var lsCate = CategoryDAO.GetListSubCateByID(r.CateID);
                foreach (var cate in lsCate)
                {
                    var ls = en.Musics.Where(w => w.SongOrMV == r.SongOrMusic && w.CateID == cate.ID).OrderByDescending(o => o.MusicView).Take(10).ToList();
                    foreach (var item in ls)
                    {
                        if (grade < 11)
                        {
                            en.RankMusics.Add(new RankMusic { MusicID = item.ID, RankID = id, RMusicGrade = grade });
                            en.SaveChanges();
                            grade++;
                        }
                        
                    }
                }
                
                return true;
            }
            return false;

        }
        public static Rank GetRankNewest(int idCate,bool? music)
        {
            var en = new ProjectNCTEntities();
            var id= en.Ranks.Where(w => w.SongOrMusic == music && w.CateID==idCate).Max(m=>m.ID);
            return en.Ranks.SingleOrDefault(s => s.ID == id);
        }
        public static Rank GetRankByID(int id)
        {
            var en = new ProjectNCTEntities();
            return en.Ranks.SingleOrDefault(w => w.ID == id);
        }
        public static IEnumerable<Rank> GetListRank(bool music)
        {
            var en = new ProjectNCTEntities();
            return en.Ranks.OrderByDescending(o => o.RMusicEnd).Where(w => w.SongOrMusic == music).ToList();
        }
        public static IEnumerable<RankMusic> GetListRM(int id)
        {
            var en = new ProjectNCTEntities();
            return en.RankMusics.OrderBy(o => o.RMusicGrade).Where(w => w.RankID == id).ToList();
        }
        public static bool DelRank(int id)
        {
            var en = new ProjectNCTEntities();
            var ls = GetListRM(id);
            foreach (var item in ls)
            {
                en.RankMusics.Remove(en.RankMusics.Find(item.ID));
                en.SaveChanges();
            }
            en.Ranks.Remove(en.Ranks.Find(id));
            return en.SaveChanges()>0?true:false;
        }
    }
}