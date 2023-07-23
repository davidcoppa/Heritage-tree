using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventsManager.Model;
using Events.Core.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace EventsManager.Data
{
    public class EventsContext : IdentityDbContext<IdentityUser>
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

            var roleAdmin = new IdentityRole()
            {
                Id = "6368d931-bb95-4a6d-9ada-ac38d97381e1",
                Name = "Admin",
                NormalizedName = "Admin"
            };

            modelBuilder.Entity<IdentityRole>().HasData(roleAdmin);

            var roleUser = new IdentityRole()
            {
                Id = "3b4a505e-964f-472c-8282-6eebf3da2c8d",
                Name = "User",
                NormalizedName = "User"
            };

            modelBuilder.Entity<IdentityRole>().HasData(roleUser);

            base.OnModelCreating(modelBuilder);


        }

        public DbSet<Event> Event { get; set; }
        public DbSet<EventTypes> EventType { get; set; }
        public DbSet<Media> Media { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<States> State { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<FileData> FileData { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<MediaType> MediaType { get; set; }
        public DbSet<UserEvent> UserEvent { get; set; }

    }
}
