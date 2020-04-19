using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.ModelViews
{
    public class QualityMusicView
    {
        public int QualityID { get; set; }
        public string QualityName { get; set; }
        public bool QualityVip { get; set; }
        //
        public int ID { get; set; }
        public int MusicID { get; set; }
        public string MusicFile { get; set; }
        public bool QMusicApproved { get; set; }
        public bool NewFile { get; set; }
        //
        public MusicView ItemMusic { get; set; }
    }
}