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
    
    public partial class LyricsMusic
    {
        public int ID { get; set; }
        public string LMusicDetail { get; set; }
        public int MusicID { get; set; }
        public int UserID { get; set; }
        public System.DateTime LMusicDayCreate { get; set; }
    
        public virtual Music Music { get; set; }
    }
}
