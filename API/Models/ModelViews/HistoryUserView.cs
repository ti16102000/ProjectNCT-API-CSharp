using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.ModelViews
{
    public class HistoryUserView
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int MusicID { get; set; }
        public MusicView ItemMusic { get; set; }
    }
}