using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.ModelViews
{
    public class MusicView
    {
        public int ID { get; set; }
        public string MusicName { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserImg { get; set; }
        public bool MusicDownloadAllowed { get; set; }
        public int View { get; set; }
        public bool SongOrMV { get; set; }
        public int CateID { get; set; }
        public string MusicImage { get; set; }
        public System.DateTime MusicDayCreate { get; set; }
        public string MusicNameUnsigned { get; set; }
        public Nullable<int> MusicRelated { get; set; }
        public IEnumerable<SingerMusicView> ListSinger { get; set; }
    }
}