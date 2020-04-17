using API.Models.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.DAO
{
    public class MusicDAO
    {
        public static int CreateMusic(Music m)
        {
            m.MusicDayCreate = DateTime.Now;
            var en = new ProjectNCTEntities();
            en.Musics.Add(m);
            en.SaveChanges();
            return m.ID;
        }
        public static Music GetMusicByID(int id)
        {
            var en =new ProjectNCTEntities();
            return en.Musics.Find(id) ?? null;
        }
        public static IEnumerable<Music> GetListMusicForSinger(int id,bool music)
        {
            var en = new ProjectNCTEntities();
            return en.Musics.Where(w => w.SingerMusics.Any(w1 => w1.SingerID == id) && w.SongOrMV==music && w.User.Role.ID==2).ToList() ?? null;
        }
        public static IEnumerable<Music> GetListMusicForUser(int id, bool music)
        {
            var en = new ProjectNCTEntities();
            return en.Musics.Where(w => w.UserID == id && w.SongOrMV == music).ToList();
        }
        public static IEnumerable<Music> GetListMusicByIDCate(int id,bool music)
        {
            var en = new ProjectNCTEntities();
            return en.Musics.Where(w => w.CateID == id && w.SongOrMV == music).ToList() ?? null;
        }
        public static IEnumerable<Music> GetListMusicRandomByIDCate(int id, bool music)
        {
            var en = new ProjectNCTEntities();
            return en.Musics.Where(w => w.CateID == id && w.SongOrMV == music).OrderBy(s => Guid.NewGuid()).Take(5).ToList() ?? null;
        }
        public static IEnumerable<Music> GetListMusic(bool music)
        {
            var en = new ProjectNCTEntities();
            return en.Musics.Where(w => w.SongOrMV == music).ToList() ?? null;
        }
        public static bool UpdateMusic(Music m)
        {
            var en = new ProjectNCTEntities();
            var item = en.Musics.SingleOrDefault(s => s.ID == m.ID);
            item.MusicName = m.MusicName;
            item.MusicDownloadAllowed = m.MusicDownloadAllowed;
            item.MusicImage = m.MusicImage;
            item.SongOrMV = m.SongOrMV;
            item.UserID = m.UserID;
            item.CateID = m.CateID;
            item.MusicNameUnsigned = m.MusicNameUnsigned;
            return en.SaveChanges() > 0 ? true : false;
        }
        public static bool UpdateRelated(int id, int idRelated)
        {
            var en = new ProjectNCTEntities();
            var item = en.Musics.SingleOrDefault(s => s.ID == id);
            if (idRelated == 0)
            {
                item.MusicRelated = null;
            }
            else
            {
                item.MusicRelated = idRelated;
            }
            
            return en.SaveChanges() > 0 ? true : false;
        }
        public static bool DelMusic(int id)
        {
            var en = new ProjectNCTEntities();
            var item = en.Musics.Find(id);
            en.Musics.Remove(item);
            return en.SaveChanges() > 0 ? true : false;
        }
        public static int UpdateView(int id)
        {
            var en = new ProjectNCTEntities();
            var item = en.Musics.Find(id);
            var viewUp= item.MusicView + 1;
            item.MusicView = viewUp;
            return en.SaveChanges() > 0 ? viewUp : 0;

        }
        public static IEnumerable<Music> GetListMusicSearch(string value,bool music)
        {
            var en = new ProjectNCTEntities();
            var ls = en.Musics.Where(w => w.SongOrMV==music && (w.MusicName.ToLower().Contains(value.ToLower()) || w.MusicNameUnsigned.ToLower().Contains(value.ToLower()))).ToList();
            return ls ?? null;
        }
    }
}