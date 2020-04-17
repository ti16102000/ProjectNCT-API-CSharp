//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace API.Models.ModelEntities
{
    using System;
    using System.Collections.Generic;
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.Musics = new HashSet<Music>();
            this.OrderVips = new HashSet<OrderVip>();
            this.SingerMusics = new HashSet<SingerMusic>();
        }
    
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
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Music> Musics { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderVip> OrderVips { get; set; }
        public virtual Role Role { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SingerMusic> SingerMusics { get; set; }
    }
}
