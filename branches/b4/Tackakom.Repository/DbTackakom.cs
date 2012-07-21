using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Tackakom.Model;

namespace Tackakom.Repository
{
    public class DbTackakom : DbContext
    {
        public DbSet<Host> Hosts { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventCategory> EventCategories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Event>()
            //    .HasMany(w => w.IconId).WithMany()
            //    .Map(map=>map.ToTable("dboIcons")
            //    .MapLeftKey("")
            //modelBuilder.Entity<User>().ToTable("aspnet_Users");
            base.OnModelCreating(modelBuilder);
        }
        
    }
}
