using API.Models.ModelEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace API.Models.DAO
{
    public class UserDAO
    {
        public static int CreateUser(User u)
        {

            ProjectNCTEntities en = new ProjectNCTEntities();
            u.UserDayCreate = DateTime.Now;
            u.UserVIP = false;
            u.DayVipEnd = null;
            u.UserActive = true;
            en.Users.Add(u);
            var checkMail = en.Users.SingleOrDefault(w => w.UserEmail == u.UserEmail);
            if (checkMail != null)
            {
                return 0;
            }
            en.SaveChanges(); 
            return u.ID;
        }
        public static User GetUserByID(int id)
        {
            ProjectNCTEntities en = new ProjectNCTEntities();
            User usr = en.Users.Find(id);
            if (usr != null)
            {
                if (usr.UserVIP == true)
                {
                    if (usr.DayVipEnd > DateTime.Now)
                    {
                        usr.UserVIP = false;
                        en.SaveChanges();
                    }
                }
                return usr;
            }
            return null;
        }
        public static IEnumerable<User> GetListSinger()
        {
            ProjectNCTEntities en = new ProjectNCTEntities();
            return en.Users.Where(w => w.RoleID == 2).ToList() ?? null;
        }
        public static IEnumerable<User> GetListUser(bool vip)
        {
            ProjectNCTEntities en = new ProjectNCTEntities();
            return en.Users.Where(w => w.RoleID == 3 && w.UserVIP == vip).ToList() ?? null;
        }
        public static IEnumerable<User> GetListSingerSearch(string value)
        {
            ProjectNCTEntities en = new ProjectNCTEntities();
            return en.Users.Where(w =>w.RoleID==2 && (w.UserName.ToLower().Contains(value.ToLower()) || w.UserNameUnsigned.ToLower().Contains(value.ToLower()))).ToList() ?? null;
        }
        public static bool UpdateUser(User user)
        {
            ProjectNCTEntities en = new ProjectNCTEntities();
            User u = en.Users.SingleOrDefault(w => w.ID == user.ID);
            u.UserDescription = user.UserDescription;
            u.UserDOB = user.UserDOB;
            u.UserGender = user.UserGender;
            u.UserImage = user.UserImage;
            u.UserName = user.UserName;
            u.UserNameUnsigned = user.UserNameUnsigned;
            return en.SaveChanges() > 0 ? true : false;
        }
        public static User LoginNormal(string mail, string pwd)
        {
            ProjectNCTEntities en = new ProjectNCTEntities();
            return en.Users.SingleOrDefault(w => w.UserEmail == mail && w.UserPwd == pwd) ?? null;
        }
        public static int UpdatePwd(string mail, string pwdOld, string pwdNew)
        {
            ProjectNCTEntities en = new ProjectNCTEntities();
            User u = en.Users.SingleOrDefault(w => w.UserEmail == mail && w.UserPwd == pwdOld);
            if (u != null)
            {
                u.UserPwd = pwdNew;
                en.SaveChanges();
                return u.ID;
            }
            return 0;
        }
        public static int ChangePwd(string mail,string pwdNew)
        {
            ProjectNCTEntities en = new ProjectNCTEntities();
            User u = en.Users.SingleOrDefault(w => w.UserEmail == mail);
            if (u != null)
            {
                u.UserPwd = pwdNew;
                en.SaveChanges();
                return u.ID;
            }
            return 0;
        }
    }
}