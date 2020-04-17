using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.ModelViews
{
    public class LyricsView
    {
        public int ID { get; set; }
        public string LMusicDetail { get; set; }
        public int MusicID { get; set; }
        public int UserID { get; set; }
        public System.DateTime LMusicDayCreate { get; set; }

        public string UserName { get; set; }
    }
}