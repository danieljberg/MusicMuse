using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMuse.Models
{
    public class Musician
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Instrument { get; set; }
        public bool LookingForBand { get; set; }
        public bool WantToCollaborate { get; set; }

    }
}
