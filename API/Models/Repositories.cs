using API.Models.BUS;
using API.Models.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Repositories
    {
        #region Category
        public static bool CreateCate(Category cate)
        {
            return MusicBUS.CreateCate(cate);
        }
        public static Category GetCateByID(int id)
        {
            return MusicBUS.GetCateByID(id);
        }
        public static List<Category> GetListSuperCate()
        {
            return MusicBUS.GetListSuperCate();
        }
        public static List<Category> GetListSubCateByID(int id)
        {
            return MusicBUS.GetListSubCateByID(id);
        }
        public static List<Category> GetListSubCate()
        {
            return MusicBUS.GetListSubCate();
        }
        public static bool UpdateCate(Category cate)
        {
            return MusicBUS.UpdateCate(cate);
        }
        public static bool DelCate(int id)
        {
            return MusicBUS.DelCate(id);
        }
        #endregion

        #region Quality 
        public static bool CreateQuality(Quality q)
        {
            return MusicBUS.CreateQuality(q);
        }
        public static Quality GetQualityByID(int id)
        {
            return MusicBUS.GetQualityByID(id);
        }
        public static IEnumerable<Quality> GetListQuality()
        {
            return MusicBUS.GetListQuality();
        }
        public static bool UpdateQuality(Quality q)
        {
            return MusicBUS.UpdateQuality(q);
        }
        #endregion

        #region QualityMusic
        public static bool CreateQM(QualityMusic qm)
        {
            return MusicBUS.CreateQM(qm);
        }
        public static QualityMusic GetQMByID(int id)
        {
            return MusicBUS.GetQMByID(id);
        }
        public static QualityMusic GetQMByIDMusic(int id, bool vip)
        {
            return MusicBUS.GetQMByIDMusic(id, vip);
        }
        public static IEnumerable<QualityMusic> GetListQM(int id)
        {
            return MusicBUS.GetListQM(id);
        }
        public static bool DelQM(int id)
        {
            return MusicBUS.DelQM(id);
        }
        #endregion

        #region Playlist
        public static bool CreatePlaylist(Playlist pl)
        {
            return MusicBUS.CreatePlaylist(pl);
        }
        public static Playlist GetPlaylistByID(int id)
        {
            return MusicBUS.GetPlaylistByID(id);
        }
        public static IEnumerable<Playlist> GetListPlaylistByIDCate(int id)
        {
            return MusicBUS.GetListPlaylistByIDCate(id);
        }
        public static IEnumerable<Playlist> GetListPlaylistByIDUser(int id)
        {
            return MusicBUS.GetListPlaylistByIDUser(id);
        }
        public static IEnumerable<Playlist> Get3PlaylistByIDCate(int id)
        {
            return MusicBUS.Get3PlaylistByIDCate(id);
        }
        public static bool UpdatePlaylist(Playlist pl)
        {
            return MusicBUS.UpdatePlaylist(pl);
        }
        public static bool DelPlaylist(int id)
        {
            return MusicBUS.DelPlaylist(id);
        }
        public static IEnumerable<Playlist> GetListPlaylistSearch(string value)
        {
            return MusicBUS.GetListPlaylistSearch(value);
        }
        #endregion

        #region PlaylistMusic
        public static bool CreatePM(PlaylistMusic pm)
        {
            return MusicBUS.CreatePM(pm);
        }
        public static IEnumerable<PlaylistMusic> GetListPMByID(int id)
        {
            return MusicBUS.GetListPMByID(id);
        }
        public static bool DelPM(int id)
        {
            return MusicBUS.DelPM(id);
        }
        #endregion

        #region Music
        public static int CreateMusic(Music m)
        {
            return MusicBUS.CreateMusic(m);
        }
        public static Music GetMusicByID(int id)
        {
            return MusicBUS.GetMusicByID(id);
        }
        public static IEnumerable<Music> GetListMusicForSinger(int id, bool music)
        {
            return MusicBUS.GetListMusicForSinger(id, music);
        }
        public static IEnumerable<Music> GetListMusicForUser(int id, bool music)
        {
            return MusicBUS.GetListMusicForUser(id, music);
        }
        public static IEnumerable<Music> GetListMusicByIDCate(int id, bool music)
        {
            return MusicBUS.GetListMusicRandomByIDCate(id, music);
        }
        public static IEnumerable<Music> GetListMusicRandomByIDCate(int id, bool music)
        {
            return MusicBUS.GetListMusicRandomByIDCate(id, music);
        }
        public static IEnumerable<Music> GetListMusic(bool music)
        {
            return MusicBUS.GetListMusic(music);
        }
        public static bool UpdateMusic(Music m)
        {
            return MusicBUS.UpdateMusic(m);
        }
        public static bool UpdateRelated(int id, int idRelated)
        {
            return MusicBUS.UpdateRelated(id, idRelated);
        }
        public static bool DelMusic(int id)
        {
            return MusicBUS.DelMusic(id);
        }
        public static int UpdateView(int id)
        {
            return MusicBUS.UpdateView(id);
        }
        public static IEnumerable<Music> GetListMusicSearch(string value, bool music)
        {
            return MusicBUS.GetListMusicSearch(value,music);
        }
        #endregion

        #region User
        public static int CreateUser(User u)
        {
            return UserBUS.CreateUser(u);
        }
        public static User GetUserByID(int id)

        {
            return UserBUS.GetUserByID(id);
        }
        public static IEnumerable<User> GetListSinger()
        {
            return UserBUS.GetListSinger();
        }
        public static IEnumerable<User> GetListUser(bool vip)
        {
            return UserBUS.GetListUser(vip);
        }
        public static IEnumerable<User> GetListSingerSearch(string value)
        {
            return UserBUS.GetListSingerSearch(value);
        }
        public static bool UpdateUser(User user)
        {
            return UserBUS.UpdateUser(user);
        }
        public static User LoginNormal(string mail, string pwd)
        {
            return UserBUS.LoginNormal(mail, pwd);
        }
        public static int UpdatePwd(string mail, string pwdOld, string pwdNew)
        {
            return UserBUS.UpdatePwd(mail, pwdOld, pwdNew);
        }
        public static int ChangePwd(string mail, string pwdNew)
        {
            return UserBUS.ChangePwd(mail, pwdNew);
        }
        #endregion

        #region SingerMusic
        public static bool CreateSM(SingerMusic sm)
        {
            return MusicBUS.CreateSM(sm);
        }
        public static IEnumerable<SingerMusic> GetSMByID(int id)
        {
            return MusicBUS.GetSMByID(id);
        }
        public static bool DelSM(int id)
        {
            return MusicBUS.DelSM(id);
        }
        public static bool DelSinger(int id, int idSinger)
        {
            return MusicBUS.DelSinger(id, idSinger);
        }
        #endregion

        #region Payment
        public static bool CreatePayment(Payment p)
        {
            return PaymentBUS.CreatePayment(p);
        }
        public static IEnumerable<Payment> GetListPayment()
        {
            return PaymentBUS.GetListPayment();
        }
        public static bool DelPayment(int id)
        {
            return PaymentBUS.DelPayment(id);
        }
        #endregion

        #region PackageVip
        public static bool CreatePV(PackageVip pv)
        {
            return PaymentBUS.CreatePV(pv);
        }
        public static IEnumerable<PackageVip> GetListPV()
        {
            return PaymentBUS.GetListPV();
        }
        public static bool DelPV(int id)
        {
            return PaymentBUS.DelPV(id);
        }
        #endregion

        #region LyricsMusic
        public static bool CreateLM(LyricsMusic lm)
        {
            return MusicBUS.CreateLM(lm);
        }
        public static LyricsMusic GetLMByID(int id)
        {
            return MusicBUS.GetLMByID(id);
        }
        public static bool UpdateLM(LyricsMusic lm)
        {
            return MusicBUS.UpdateLM(lm);
        }
        #endregion

        #region Partner
        public static bool CreatePartner(Partner p)
        {
            return UserBUS.CreatePartner(p);
        }
        public static Partner GetPartnerByID(int id)
        {
            return UserBUS.GetPartnerByID(id);
        }
        public static IEnumerable<Partner> GetListPartner()
        {
            return UserBUS.GetListPartner();
        }
        public static bool DelPartner(int id)
        {
            return UserBUS.DelPartner(id);
        }
        #endregion

        #region HistoryUser
        public static bool CreateHU(HistoryUser hu)
        {
            return UserBUS.CreateHU(hu);
        }
        public static IEnumerable<HistoryUser> GetListMusicHUByIDUser(int id, bool music)
        {
            return UserBUS.GetListMusicHUByIDUser(id, music);
        }
        public static IEnumerable<HistoryUser> GetListMusicHUByIDMusic(int idUser, int idMusic)
        {
            return UserBUS.GetListMusicHUByIDMusic(idUser, idMusic);
        }
        public static bool DelMusicHU(int idUser, int idMusic)
        {
            return UserBUS.DelMusicHU(idUser, idMusic);
        }
        public static bool DelListMusicHU(int idUser, bool music)
        {
            return UserBUS.DelListMusicHU(idUser, music);
        }
        #endregion
    }
}