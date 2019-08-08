using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMuse.Models
{
    public class EventBand
    {
        [ForeignKey("Event")]
        public string EventId { get; set; }
        public Event Event { get; set; }
        [ForeignKey("Band")]
        public string BandId { get; set; }
        public Band Band { get; set; }

    }
}
