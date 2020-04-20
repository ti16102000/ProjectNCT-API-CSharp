using API.Models.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.DAO
{
    public class HistoryUserDAO
    {
        public static bool CreateHU(HistoryUser hu)
        {
            var en = new ProjectNCTEntities();
            en.HistoryUsers.Add(hu);
            return en.SaveChanges() > 0 ? true : false;
        }
        public static IEnumerable<HistoryUser> GetListMusicHUByIDUser(int idUser,bool music)
        {
            var en = new ProjectNCTEntities();
            return en.HistoryUsers.Where(w => w.UserID == idUser && w.Music.SongOrMV == music).ToList() ?? null;
        }
        public static IEnumerable<HistoryUser> GetListMusicHUByIDMusic(int idUser,int idMusic)
        {
            var en = new ProjectNCTEntities();
            return en.HistoryUsers.Where(w => w.UserID == idUser && w.MusicID == idMusic).ToList();
        }
        public static bool DelMusicHU(int id)
        {
            var en = new ProjectNCTEntities();
            var hu = en.HistoryUsers.Find(id);
            var ls = GetListMusicHUByIDMusic(hu.UserID, hu.MusicID);
            foreach (var item in ls)
            {
                en.HistoryUsers.Remove(en.HistoryUsers.Find(item.ID));
            }
            return en.SaveChanges()>0?true:false;
        }
        public static bool DelListMusicHU(int idUser,bool music)
        {
            var en = new ProjectNCTEntities();
            var ls = GetListMusicHUByIDUser(idUser, music);
            foreach (var item in ls)
            {
                en.HistoryUsers.Remove(en.HistoryUsers.Find(item.ID));
            }
            return en.SaveChanges() > 0 ? true : false;
        }
    }
}