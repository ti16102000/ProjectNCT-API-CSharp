using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.ModelViews
{
    public class SingerMusicView
    {
        public int ID { get; set; }
        public int MusicID { get; set; }
        public int SingerID { get; set; }
        public string SingerName { get; set; }
    }
}