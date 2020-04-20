using API.Models.DAO;
using API.Models.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.BUS
{
    public class UserBUS
    {
        #region User
        public static int CreateUser(User u)
        {
            return UserDAO.CreateUser(u);
        }
        public static User GetUserByID(int id)
        {
            return UserDAO.GetUserByID(id);
        }
        public static IEnumerable<User> GetListSinger()
        {
            return UserDAO.GetListSinger();
        }
        public static IEnumerable<User> GetListUser(bool vip)
        {
            return UserDAO.GetListUser(vip);
        }
        public static IEnumerable<User> GetListSingerSearch(string value)
        {
            return UserDAO.GetListSingerSearch(value);
        }
        public static bool UpdateUser(User user)
        {
            return UserDAO.UpdateUser(user);
        }
        public static User LoginNormal(string mail, string pwd)
        {
            return UserDAO.LoginNormal(mail, pwd);
        }
        public static int ChangePwd(string mail,string pwdNew)
        {
            return UserDAO.ChangePwd(mail, pwdNew);
        }
        #endregion

        #region Partner
        public static bool CreatePartner(Partner p)
        {
            return PartnerDAO.CreatePartner(p);
        }
        public static Partner GetPartnerByID(int id)
        {
            return PartnerDAO.GetPartnerByID(id);
        }
        public static IEnumerable<Partner> GetListPartner()
        {
            return PartnerDAO.GetListPartner();
        }
        public static bool DelPartner(int id)
        {
            return PartnerDAO.DelPartner(id);
        }
        #endregion

        #region HistoryUser
        public static bool CreateHU(HistoryUser hu)
        {
            return HistoryUserDAO.CreateHU(hu);
        }
        public static IEnumerable<HistoryUser> GetListMusicHUByIDUser(int id, bool music)
        {
            return HistoryUserDAO.GetListMusicHUByIDUser(id, music);
        }
        public static IEnumerable<HistoryUser> GetListMusicHUByIDMusic(int idUser, int idMusic)
        {
            return HistoryUserDAO.GetListMusicHUByIDMusic(idUser, idMusic);
        }
        public static bool DelMusicHU(int id)
        {
            return HistoryUserDAO.DelMusicHU(id);
        }
        public static bool DelListMusicHU(int idUser, bool music)
        {
            return HistoryUserDAO.DelListMusicHU(idUser, music);
        }
        #endregion
    }
}