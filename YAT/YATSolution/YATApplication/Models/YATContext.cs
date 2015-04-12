using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace YATApplication.Models
{
    public class YATContext : DbContext
    {
        public DbSet<UserProfile> UserProfile { get; set; }
        public DbSet<Connections> Connections { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserProfile>()
                .HasMany(t => t.Likes)
                .WithMany(t => t.Users);
        }
    }
}

