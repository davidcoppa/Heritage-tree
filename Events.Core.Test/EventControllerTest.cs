using AutoMapper;
using Events.Core.Common;
using Events.Core.Controllers;
using Events.Core.DTOs;
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
            Id=1,
            Loccation=new Model.Location { Id=1,Country="US"},
            Person1=new Person {Id=1,FirstName="pepe" },
            Title="the test"
            
        };

        EventCreateDTO evtCreateNull =new EventCreateDTO();

        EventCreateDTO evtFull = new EventCreateDTO
        {
            Description = "event test full",
            EventDate = DateTime.Now,
            EventType = new EventTypes { Id = 1, Description = "test", Name = "test" },
            Loccation = new Model.Location { Id = 1, Country = "US" },
            Person1 = new Person { Id = 1, FirstName = "son" },
            Person2 = new Person { Id = 2, FirstName = "ana" },
            Person3 = new Person { Id = 3, FirstName = "juan" },
            Title = "the test",
            photos=new List<Photos> 
            {
                new Photos{ Id=1, Date=DateTime.Now ,Name="1.jpg",Description="photo1",UrlFile="c:/data1"},
                new Photos{ Id=2, Date=DateTime.Now ,Name="2.jpg",Description="photo2",UrlFile="c:/data2"},
            }

        };

        EventCreateDTO ppCreateNoMother = new EventCreateDTO
        {
            Description = "event test full",
            EventDate = DateTime.Now,
            EventType = new EventTypes { Id = 1, Description = "test", Name = "test" },
            Loccation = new Model.Location { Id = 1, Country = "US" },
            Person1 = new Person { Id = 1, FirstName = "son" },
            Person2 = new Person { Id = 2, FirstName = "ana" },
            Person3 = new Person { Id = 3, FirstName = "juan" },
            Title = "the test",
            photos = new List<Photos>
            {
                new Photos{ Id=1, Date=DateTime.Now ,Name="1.jpg",Description="photo1",UrlFile="c:/data1"},
                new Photos{ Id=2, Date=DateTime.Now ,Name="2.jpg",Description="photo2",UrlFile="c:/data2"},
            }

        };
        EventCreateDTO ppCreateNoSon = new EventCreateDTO
        {
            Description = "event test full",
            EventDate = DateTime.Now,
            EventType = new EventTypes { Id = 1, Description = "test", Name = "test" },
            Loccation = new Model.Location { Id = 1, Country = "US" },
           // Person1 = new Person { ID = 1, FirstName = "son" },
            Person2 = new Person { Id = 2, FirstName = "ana" },
            Person3 = new Person { Id= 3, FirstName = "juan" },
            Title = "the test",
            photos = new List<Photos>
            {
                new Photos{ Id=1, Date=DateTime.Now ,Name="1.jpg",Description="photo1",UrlFile="c:/data1"},
                new Photos{ Id=2, Date=DateTime.Now ,Name="2.jpg",Description="photo2",UrlFile="c:/data2"},
            }

        };

        Person pSon = new() { Id = 1, FirstName = "son" };
        Person pSonBad = new() { Id = 1};
        Person pMother = new() { Id = 2, FirstName = "ana" };
        Person pFather = new() { Id = 3, FirstName = "juan" };
        Model.Location location = new Model.Location() { Id = 1, Country = "US" };
        Photos photo1 = new Photos { Id = 1, Date = DateTime.Now, Name = "1.jpg", Description = "photo1", UrlFile = "c:/data1" };
        Photos photo2 = new Photos { Id = 2, Date = DateTime.Now, Name = "2.jpg", Description = "photo2", UrlFile = "c:/data2" };
        EventTypes eventypes = new EventTypes { Id = 1, Description = "test", Name = "test" };

        EventCreateEditDTO evtEditGood = new () 
        {
            ID=1,
            Description = "Edit event test full",
            EventDate = DateTime.Now,
            EventType = new EventTypes { Id = 1, Description = "test", Name = "test" },
            Loccation = new Model.Location { Id = 1, Country = "US" },
            Person1 = new Person { Id = 1, FirstName = "son" },
            Person2 = new Person { Id = 2, FirstName = "ana" },
            Person3 = new Person { Id = 3, FirstName = "juan" },
            Title = "the test",
            photos = new List<Photos>
            {
                new Photos{ Id=1, Date=DateTime.Now ,Name="1.jpg",Description="photo1",UrlFile="c:/data1"},
                new Photos{ Id=2, Date=DateTime.Now ,Name="2.jpg",Description="photo2",UrlFile="c:/data2"},
            }
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

        [TestMethod]
        public async Task GetByIdFound()
        {
            var profile = new AutoMapperProfiles();
            var mapConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(mapConfiguration);

            var validator = new ValidatorsMoq();


            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;


            using (var context = CreateContext(options))
            {
                context.Event.Add(evt);

                context.SaveChanges();

                var testResul = new EventController(context, mapper, validator);
                IActionResult countResult = await testResul.Details(1);

                var contentResult = countResult as OkObjectResult;

                var value = contentResult?.Value as Event;
                Assert.IsNotNull(value);
                Assert.AreEqual(1, value.Id);

            }
        }

        [TestMethod]
        public async Task GetByIdNotFound()
        {
            var profile = new AutoMapperProfiles();
            var mapConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(mapConfiguration);

            var validator = new ValidatorsMoq();

            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            using (var context = CreateContext(options))
            {
                var testResul = new EventController(context, mapper, validator);
                IActionResult countResult = await testResul.Details(5);


                Assert.IsInstanceOfType(countResult, typeof(NotFoundResult));

            }
        }

        [TestMethod]
        public async Task CreateAllItemsRigthFull()
        {
            var profile = new AutoMapperProfiles();
            var mapConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(mapConfiguration);

            var validator = new ValidatorsMoq();

            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            using (var context = CreateContext(options))
            {
                context.SaveChanges();

                var testResul = new EventController(context, mapper, validator);
                IActionResult countResult = await testResul.Create(evtFull);

                var contentResult = countResult as OkObjectResult;

                var value = contentResult?.Value as Event;
                Assert.IsNotNull(value);
                Assert.AreEqual(1, value.Id);

            }
        }

        [TestMethod]
        public async Task CreateNullObject()
        {
            var profile = new AutoMapperProfiles();
            var mapConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(mapConfiguration);
            var validator = new ValidatorsMoq();

            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            using (var context = CreateContext(options))
            {

                context.SaveChanges();

                var testResul = new EventController(context, mapper, validator);
                IActionResult countResult = await testResul.Create(evtCreateNull);

                var contentResult = countResult as BadRequestObjectResult;

                Assert.IsInstanceOfType(countResult, typeof(BadRequestObjectResult));
                Assert.IsNotNull(contentResult);
                Assert.AreEqual("Model is null or not valid", contentResult.Value);

            }
        }

        [TestMethod]
        public async Task CreateNullMother()
        {
            var profile = new AutoMapperProfiles();
            var mapConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(mapConfiguration);

            var validator = new ValidatorsMoq();

            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            using (var context = CreateContext(options))
            {
                var testResul = new EventController(context, mapper, validator);
                IActionResult countResult = await testResul.Create(ppCreateNoMother);

                var contentResult = countResult as OkObjectResult;

                var value = contentResult?.Value as Event;
                Assert.IsNotNull(value);
                Assert.AreEqual(1, value.Id);

            }
        }

        [TestMethod]
        public async Task CreateNotFoundSon()
        {
            var profile = new AutoMapperProfiles();
            var mapConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(mapConfiguration);

            var validator = new ValidatorsMoq();

            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            using (var context = CreateContext(options))
            {
                var testResul = new EventController(context, mapper, validator);
                IActionResult countResult = await testResul.Create(ppCreateNoSon);

                var contentResult = countResult as BadRequestObjectResult;

                Assert.IsInstanceOfType(countResult, typeof(BadRequestObjectResult));
                Assert.IsNotNull(contentResult);
                Assert.AreEqual("An event needs at least one person", contentResult.Value);

            }
        }

        [TestMethod]
        public async Task EditExistingEventRightValues()
        {
            var profile = new AutoMapperProfiles();
            var mapConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(mapConfiguration);
            var validator = new ValidatorsMoq();

            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            using (var context = CreateContext(options))
            {
                context.Event.Add(evt);

                context.SaveChanges();

                var testResul = new EventController(context, mapper, validator);
                IActionResult countResult = await testResul.Edit(1, evtEditGood);

                var contentResult = countResult as OkObjectResult;

                var value = contentResult?.Value as Event;
                Assert.IsNotNull(value);
                Assert.AreEqual("Edit event test full", value.Description);

            }
        }


        [TestMethod]
        public async Task DeletePerson()
        {
            var profile = new AutoMapperProfiles();
            var mapConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(mapConfiguration);

            var validator = new ValidatorsMoq();


            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            using (var context = CreateContext(options))
            {
                context.Event.Add(evt);

                context.SaveChanges();

                var testResul = new EventController(context, mapper, validator);
                IActionResult countResult = await testResul.Delete(1);

                Assert.IsInstanceOfType(countResult, typeof(NoContentResult));


            }
        }


    }
}
