using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventsManager.Model;
using Events.Core.Model;

namespace EventsManager.Data
{
    public class EventsContext : DbContext
    {
        private readonly Action<EventsContext, ModelBuilder> _customizeModel;

        public EventsContext(DbContextOptions<EventsContext> options)
            : base(options)
        {
        }
        //used in test project
        public EventsContext(DbContextOptions<EventsContext> options, Action<EventsContext, ModelBuilder> customizeModel)
              : base(options)
        {
            // customizeModel must be the same for every instance in a given application.
            // Otherwise a custom IModelCacheKeyFactory implementation must be provided.
            _customizeModel = customizeModel;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {




        }

        public DbSet<Event> Event { get; set; }
        public DbSet<EventTypes> EventType { get; set; }
        public DbSet<Photos> Photos { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Events.Core.Model.Location> Location { get; set; }
        public DbSet<EventsManager.Model.ParentPerson> ParentPerson { get; set; }

    }
}
