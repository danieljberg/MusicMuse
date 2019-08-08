using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicMuse.Models;

namespace MusicMuse.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole, string>
    {
        public DbSet<Band> Band { get; set; }
        public DbSet<Business> Business { get; set; }
        public DbSet<Musician> Musician { get; set; }
        public DbSet<ApplicationRole> Roll { get; set; }
        public DbSet<ApplicationUser> User { get; set; }
        public DbSet<Influence> Influence { get; set; }
        public DbSet<Event> Event { get; set; }
        public DbSet<MusicianBandInfluenceScore> MusicianBandInfluenceScore { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        
    }
}
