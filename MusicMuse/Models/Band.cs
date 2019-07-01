using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMuse.Models
{
    public class Band
    {
        [Key]
        public int Id { get; set; }
        public string BandName { get; set; }
        public string MemberLookingFor { get; set; }
        public bool LookingToBeHired { get; set; }


    }
}
