using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMuse.Models
{
    public class Musician
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "What is the main instrument you play?")]
        public string Instrument { get; set; }
        [Display(Name = "What are your top 3 influences?\nFirst being your main influence")]
        public string Influence1 { get; set; }
        [Display(Name = "Second Influence")]
        public string Influence2 { get; set; }
        [Display(Name = "Third Influence")]
        public string Influence3 { get; set; }
        [Display(Name = "Are you looking for a band?\nCheck box if you are.")]
        public bool LookingForBand { get; set; }
        [Display(Name = "Are you looking to collaborate with other musicians?\nCheck box if you are.")]
        public bool WantToCollaborate { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
