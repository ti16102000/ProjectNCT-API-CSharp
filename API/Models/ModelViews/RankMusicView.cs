using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.ModelViews
{
    public class RankMusicView
    {
        public int ID { get; set; }
        public int RMusicGrade { get; set; }
        public MusicView ItemMusic { get; set; }
    }
}