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
        public string Venue { get; set; }
        [Display(Name = "Event Information")]
        public string EventInfo { get; set; }
        [ForeignKey("Business")]
        public int BusinessId { get; set; }
        public Business Business { get; set; }
        public DateTime Posted { get; set; }
        [Display(Name = "Frist Band")]
        public string Influence1 { get; set; }
        [Display(Name = "Second Band ")]
        public string Influence2 { get; set; }
        [Display(Name = "Third Band")]
        public string Influence3 { get; set; }

    }
}
