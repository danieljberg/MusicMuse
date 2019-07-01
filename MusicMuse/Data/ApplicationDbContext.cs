using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicMuse.Models;

namespace MusicMuse.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MusicMuse.Models.Band> Band { get; set; }
        public DbSet<MusicMuse.Models.Business> Business { get; set; }
        public DbSet<MusicMuse.Models.Musician> Musician { get; set; }
    }
}
