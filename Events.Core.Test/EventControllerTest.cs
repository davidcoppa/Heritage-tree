using AutoMapper;
using Events.Core.Common;
using Events.Core.Controllers;
using Events.Core.Test.Helpers;
using EventsManager.Data;
using EventsManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Core.Test
{
    [TestClass]

    public class EventControllerTest:BaseTest
    {
        private static EventsContext CreateContext(DbContextOptions<EventsContext> options)
        {
            return new EventsContext(options, (context, modelBuilder) =>
            {
                modelBuilder.Entity<EventController>();
                //modelBuilder.Entity<ParentPerson>();
                //modelBuilder.Entity<Person>();
            });
        }
        Event evt = new Event 
        {
            Description ="event test",
            EventDate = DateTime.Now,
            EventType=  new EventTypes { Id=1,Description="test", Name="test"},
            ID=1,
            Loccation=new Model.Location { Id=1,Country="US"},
            Person1=new Person {ID=1,FirstName="pepe" },
            Title="the test"
            
        };
        [TestMethod]
        public async Task GetAllTest()
        {
            var validator = new ValidatorsMoq();

            var profile = new AutoMapperProfiles();
            var mapConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(mapConfiguration);

            var options = new DbContextOptionsBuilder<EventsContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;

            using (var context = CreateContext(options))
            {
                context.Event.Add(evt);

                context.SaveChanges();

                var testResul = new EventController(context, mapper, validator);
                IActionResult countResult = await testResul.Index();

                var contentResult = countResult as OkObjectResult;

                var value = contentResult?.Value as List<Event>;
                Assert.IsNotNull(value);
                Assert.AreEqual(1, value.Count);

            }
        }



    }
}
