using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventsManager.Model;

namespace EventsManager.Data
{
    public class EventsContext : DbContext
    {
        public EventsContext(DbContextOptions<EventsContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {




        }

        public DbSet<Event> Event { get; set; }
        public DbSet<EventTypes> EventType { get; set; }
        public DbSet<Photos> Photos { get; set; }
        public DbSet<Person> Person { get; set; }

    }
}
