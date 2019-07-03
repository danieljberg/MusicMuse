using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMuse.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Name of Event")]
        public string EventName { get; set; }
        [Display(Name ="Event Location")]
        public string EventLocation { get; set; }
        public string Venue { get; set; }
        

    }
}
