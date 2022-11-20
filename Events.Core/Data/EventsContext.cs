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
        public DbSet<Media> Media { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<States> State { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<ParentPerson> ParentPerson { get; set; }
        public DbSet<FileData> FileData { get; set; }

    }
}
