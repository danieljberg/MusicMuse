using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MusicMuse.Models
{
    public class Band
    {
        [Key]
        public int Id { get; set; }
        [Display(Name ="Band Name")]
        public string BandName { get; set; }
        [Display(Name ="What are your top 3 influences?\nFirst being your main influence")]
        public string Influence1 { get; set; }
        [Display(Name ="Second Influence")]
        public string Influence2 { get; set; }
        [Display(Name = "Third Influence")]
        public string Influence3 { get; set; }
        [Display(Name = "What band member are you looking for?")]
        public string MemberLookingFor { get; set; }
        [Display(Name = "Are you looking to be booked by a business?\nCheck box if you are.")]
        public bool LookingToBeHired { get; set; }
        [ForeignKey("ApplicationUser")]
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
