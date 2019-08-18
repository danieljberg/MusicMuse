using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMuse.Models
{
    public class BusinessEventViewModel
    {
        public IEnumerable<Band> Band { get; set; }
        public Event Event { get; set; }

    }
}
