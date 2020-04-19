using API.Models.DAO;
using API.Models.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.BUS
{
    public class MusicBUS
    {
        #region Category
        public static bool CreateCate(Category cate)
        {
            return CategoryDAO.CreateCate(cate);
        }
        public static Category GetCateByID(int id)
        {
            return CategoryDAO.GetCateByID(id);
        }
        public static List<Category> GetListSuperCate()
        {
            return CategoryDAO.GetListSuperCate();
        }
        public static List<Category> GetListSubCateByID(int id)
        {
            return CategoryDAO.GetListSubCateByID(id);
        }
        public static List<Category> GetListSubCate()
        {
            return CategoryDAO.GetListSubCate();
        }
        public static bool UpdateCate(Category cate)
        {
            return CategoryDAO.UpdateCate(cate);
        }
        public static bool DelCate(int id)
        {
            return CategoryDAO.DelCate(id);
        }
        #endregion

        #region Quality 
        public static bool CreateQuality(Quality q)
        {
            return QualityDAO.CreateQuality(q);
        }
        public static Quality GetQualityByID(int id)
        {
            return QualityDAO.GetQualityByID(id);
        }
        public static IEnumerable<Quality> GetListQuality()
        {
            return QualityDAO.GetListQuality();
        }
        public static bool UpdateQuality(Quality q)
        {
            return QualityDAO.UpdateQuality(q);
        }
        #endregion

        #region QualityMusic
        public static bool CreateQM(QualityMusic qm)
        {
            return QualityMusicDAO.CreateQM(qm);
        }
        public static QualityMusic GetQMByID(int id)
        {
            return QualityMusicDAO.GetQMByID(id);
        }
        public static QualityMusic GetQMByIDMusic(int id, bool vip)
        {
            return QualityMusicDAO.GetQMByIDMusic(id, vip);
        }
        public static IEnumerable<QualityMusic> GetListQM(int id)
        {
            return QualityMusicDAO.GetListQM(id);
        }
        public static bool DelQM(int id)
        {
            return QualityMusicDAO.DelQM(id);
        }
        public static IEnumerable<QualityMusic> GetListNewFileMusic()
        {
            return QualityMusicDAO.GetListNewFileMusic();
        }
        public static bool ApproveFile(int id)
        {
            return QualityMusicDAO.ApproveFile(id);
        }
        public static bool DelFileAndTableRelated(int id)
        {
            return QualityMusicDAO.DelFileAndTableRelated(id);
        }
        #endregion

        #region Playlist
        public static bool CreatePlaylist(Playlist pl)
        {
            return PlaylistDAO.CreatePlaylist(pl);
        }
        public static Playlist GetPlaylistByID(int id)
        {
            return PlaylistDAO.GetPlaylistByID(id);
        }
        public static IEnumerable<Playlist> GetListPlaylistByIDCate(int id)
        {
            return PlaylistDAO.GetListPlaylistByIDCate(id);
        }
        public static IEnumerable<Playlist> GetListPlaylistByIDUser(int id)
        {
            return PlaylistDAO.GetListPlaylistByIDUser(id);
        }
        public static IEnumerable<Playlist> Get3PlaylistByIDCate(int id)
        {
            return PlaylistDAO.Get3PlaylistByIDCate(id);
        }
        public static bool UpdatePlaylist(Playlist pl)
        {
            return PlaylistDAO.UpdatePlaylist(pl);
        }
        public static IEnumerable<Playlist> GetListPlaylistSearch(string value)
        {
            return PlaylistDAO.GetListPlaylistSearch(value);
        }
        public static bool DelPlaylist(int id)
        {
            return PlaylistDAO.DelPlaylist(id);
        }
        #endregion

        #region PlaylistMusic
        public static bool CreatePM(PlaylistMusic pm)
        {
            return PlaylistMusicDAO.CreatePM(pm);
        }
        public static IEnumerable<PlaylistMusic> GetListPMByID(int id)
        {
            return PlaylistMusicDAO.GetListPM(id);
        }
        public static bool DelPM(int id)
        {
            return PlaylistMusicDAO.DelPM(id);
        }
        #endregion

        #region Music
        public static int CreateMusic(Music m)
        {
            return MusicDAO.CreateMusic(m);
        }
        public static Music GetMusicByID(int id)
        {
            return MusicDAO.GetMusicByID(id);
        }
        public static IEnumerable<Music> GetListMusicForSinger(int id, bool music)
        {
            return MusicDAO.GetListMusicForSinger(id, music);
        }
        public static IEnumerable<Music> GetListMusicForUser(int id, bool music)
        {
            return MusicDAO.GetListMusicForUser(id, music);
        }
        public static IEnumerable<Music> GetListMusicByIDCate(int id, bool music)
        {
            return MusicDAO.GetListMusicRandomByIDCate(id, music);
        }
        public static IEnumerable<Music> GetListMusicRandomByIDCate(int id, bool music)
        {
            return MusicDAO.GetListMusicRandomByIDCate(id, music);
        }
        public static IEnumerable<Music> GetListMusic(bool music)
        {
            return MusicDAO.GetListMusic(music);
        }
        public static bool UpdateMusic(Music m)
        {
            return MusicDAO.UpdateMusic(m);
        }
        public static bool UpdateRelated(int id, int idRelated)
        {
            return MusicDAO.UpdateRelated(id, idRelated);
        }
        public static bool DelMusic(int id)
        {
            return MusicDAO.DelMusic(id);
        }
        public static int UpdateView(int id)
        {
            return MusicDAO.UpdateView(id);
        }
        public static IEnumerable<Music> GetListMusicSearch(string value, bool music)
        {
            return MusicDAO.GetListMusicSearch(value, music);
        }
        #endregion

        #region SingerMusic
        public static bool CreateSM(SingerMusic sm)
        {
            return SingerMusicDAO.CreateSM(sm);
        }
        public static IEnumerable<SingerMusic> GetSMByID(int id)
        {
            return SingerMusicDAO.GetSMByID(id);
        }
        public static bool DelSM(int id)
        {
            return SingerMusicDAO.DelSM(id);
        }
        public static bool DelSinger(int id, int idSinger)
        {
            return SingerMusicDAO.DelSinger(id, idSinger);
        }
        #endregion

        #region LyricsMusic
        public static bool CreateLM(LyricsMusic lm)
        {
            return LyricsMusicDAO.CreateLM(lm);
        }
        public static LyricsMusic GetLMByID(int id)
        {
            return LyricsMusicDAO.GetLMByID(id);
        }
        public static bool UpdateLM(LyricsMusic lm)
        {
            return LyricsMusicDAO.UpdateLM(lm);
        }
        #endregion

        #region RankMusic
        public static bool CreateRank(Rank r)
        {
            return RankDAO.CreateRank(r);

        }
        public static Rank GetRankNewest(int idCate,bool? music)
        {
            return RankDAO.GetRankNewest( idCate,music);
        }
        public static IEnumerable<Rank> GetListRank(bool music)
        {
            return RankDAO.GetListRank(music);
        }
        public static IEnumerable<RankMusic> GetListRM(int id)
        {
            return RankDAO.GetListRM(id);
        }
        public static bool DelRank(int id)
        {
            return RankDAO.DelRank(id);
        }
        public static Rank GetRankByID(int id)
        {
            return RankDAO.GetRankByID(id);
        }
        #endregion
    }
}