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
           // modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>(); 
            //setup foreign key since FromId and ToId do not follow EF naming convention
            modelBuilder.Entity<Connections>()
                .HasRequired(c => c.From)
                .WithMany(u => u.FromConnections)
                .HasForeignKey(c => c.FromId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Connections>()
                .HasRequired(c => c.To)
                .WithMany(u => u.ToConnections)
                .HasForeignKey(c => c.ToId)
                .WillCascadeOnDelete(false);

            //setup foreign key since FromId and ToId do not follow EF naming convention
            modelBuilder.Entity<Message>()
                .HasRequired(m => m.To)
                .WithMany(u => u.ToMessages)
                .HasForeignKey(m => m.ToId)
                .WillCascadeOnDelete(false);
            modelBuilder.Entity<Message>()
                .HasRequired(m => m.From)
                .WithMany(u => u.FromMessages)
                .HasForeignKey(m => m.FromId)
                .WillCascadeOnDelete(false);
        }
    }
}

