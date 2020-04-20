using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.ModelViews
{
    public class PlaylistMusicView
    {
        public int ID { get; set; }
        public int PlaylistID { get; set; }
        public int MusicID { get; set; }
        //
        public string MusicName { get; set; }
        public bool MusicDownloadAllowed { get; set; }
        public int MusicView { get; set; }
        public IEnumerable<SingerMusicView> ListSinger { get; set; }
    }
}