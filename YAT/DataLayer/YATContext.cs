using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace DataLayer
{
    public class YATContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Connections> Connections { get; set; }
        public DbSet<Likes> Likes { get; set; }
        public DbSet<Message> Messages { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>(); 
            modelBuilder.Entity<User>()
                .HasMany(t => t.Likes)
                .WithMany(t => t.Users);
            modelBuilder.Entity<Likes>()
                .HasMany(t => t.Users)
                .WithMany(t => t.Likes);
            modelBuilder.Entity<Message>()
                .HasRequired(m => m.To).
                WithMany(t => t.ToMessages).
                HasForeignKey(m => m.ToId).
                WillCascadeOnDelete(false);
            modelBuilder.Entity<Message>()
                .HasRequired(m => m.From)
                .WithMany(t => t.FromMessages)
                .HasForeignKey(m => m.FromId)
                .WillCascadeOnDelete(false);
        }
    }
}

