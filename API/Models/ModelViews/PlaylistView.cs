using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.ModelViews
{
    public class PlaylistView
    {
        public int ID { get; set; }
        public string PlaylistName { get; set; }
        public int UserID { get; set; }
        public Nullable<int> CateID { get; set; }
        public string PlaylistImage { get; set; }
        public string PlaylistDescription { get; set; }
        public string CateName { get; set; }
        public string UserName { get; set; }
        public string UserImg { get; set; }
    }
}