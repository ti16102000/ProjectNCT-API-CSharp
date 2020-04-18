using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.ModelViews
{
    public class RankView
    {
        public int ID { get; set; }
        public string RMusicName { get; set; }
        public System.DateTime RMusicStart { get; set; }
        public System.DateTime RMusicEnd { get; set; }
        public int CateID { get; set; }
        public Nullable<bool> SongOrMusic { get; set; }
        public string CateName { get; set; }
    }
}