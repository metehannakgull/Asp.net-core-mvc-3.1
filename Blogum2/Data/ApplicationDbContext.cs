using Blogum2.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogum2.Data
{
    public class ApplicationDbContext : IdentityDbContext<Ziyaretci>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ilce> Ilce { get; set; }
        public DbSet<KampYeri> KampYeri { get; set; }
        public DbSet<SehirdekiYer> SehirdekiYer { get; set; }
        public DbSet<Tur> Tur { get; set; }
        public DbSet<Il> Il { get; set; }

        public DbSet<IlKamp> IlKamp { get; set; }
        public DbSet<IlSehirYeri> IlSehirYeri { get; set; }


    }
}
