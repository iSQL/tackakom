using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using Tackakom.Model;

namespace Tackakom.Repository
{
     public class Tackakom : DbContext
    {
         public DbSet<Host> Hosts { get; set; }
         public DbSet<Event> Events { get; set; }
         public DbSet<EventCategory> EventCategories { get; set; }
         public DbSet<Icon> Icons { get; set; } 
    }
}
