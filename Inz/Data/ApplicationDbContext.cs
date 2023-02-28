using Inz.Areas.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inz.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Lol> Lol { get; set; }
        public DbSet<Community> Community { get; set; }
        public DbSet<Cs> Cs { get; set; }
        public DbSet<Fortnite> Fortnite { get; set; }
        public DbSet<Siatkowka> Siatkowka { get; set; }
        public DbSet<Koszykowka> Koszykowka { get; set; }
        public DbSet<PilkaNozna> PilkaNozna { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
