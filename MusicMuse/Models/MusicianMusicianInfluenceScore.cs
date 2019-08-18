using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMuse.Models
{
    public class MusicianMusicianInfluenceScore
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Musician")]
        public int MusicianId { get; set; }
        public int MusicianToCheckId { get; set; }
        public Musician Musician { get; set; }
        public int InfluenceScore { get; set; }
    }
}
