using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.ModelViews
{
    public class UserView
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public Nullable<System.DateTime> UserDOB { get; set; }
        public Nullable<bool> UserGender { get; set; }
        public Nullable<bool> UserVIP { get; set; }
        public string UserEmail { get; set; }
        public string UserPwd { get; set; }
        public string UserImage { get; set; }
        public string UserDescription { get; set; }
        public string UserNameUnsigned { get; set; }
        public System.DateTime UserDayCreate { get; set; }
        public int RoleID { get; set; }
        public bool UserActive { get; set; }
        public Nullable<System.DateTime> DayVipEnd { get; set; }
        public string TokenUser { get; set; }
    }
}