using AutoMapper;
using Events.Core.Common;
using Events.Core.Common.Validators;
using Events.Core.Controllers;
using Events.Core.DTOs;
using Events.Core.Test.Helpers;
using EventsManager.Data;
using EventsManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;

namespace Events.Core.Test
{
    [TestClass]
    public class PeopleControllerTest : BaseTest
    {
        private static EventsContext CreateContext(DbContextOptions<EventsContext> options)
        {
            return new EventsContext(options, (context, modelBuilder) =>
            {
                modelBuilder.Entity<Person>();
            });

        }
        Person p = new Person
        {
            Id = 1,
            FirstName = "Test",
            DateOfBirth = DateTime.Today.AddDays(-1000),
            DateOfDeath = DateTime.Today,
            FirstSurname = "TestSurname",
            Order = 1,
            Photos = null,
            PlaceOfBirth = "cloud",
            PlaceOfDeath = "some pc",
            SecondName = "secondName",
            SecondSurname = "secondSurname",
            Sex = EventsManager.Enums.Gender.Male

        };
        PersonCreateDTO pCreate = new PersonCreateDTO
        {
            FirstName = "TestCreate",
            DateOfBirth = DateTime.Today.AddDays(-1000),
            DateOfDeath = DateTime.Today,
            FirstSurname = "TestSurname",
            Order = 2,
            Photos = null,
            PlaceOfBirth = "cloud",
            PlaceOfDeath = "some pc",
            SecondName = "secondName",
            SecondSurname = "secondSurname",
            Sex = EventsManager.Enums.Gender.Male

        };
        PersonCreateDTO pCreateNull = new PersonCreateDTO { };

        PersonEditDTO pEditGood = new PersonEditDTO
        {
            Id = 1,
            FirstName = "NewName",
            DateOfBirth = DateTime.Today.AddDays(-1000),
            DateOfDeath = DateTime.Today,
            FirstSurname = "TestSurname",
            Order = 1,
            Photos = null,
            PlaceOfBirth = "cloud",
            PlaceOfDeath = "some pc",
            SecondName = "secondName",
            SecondSurname = "secondSurname",
            Sex = EventsManager.Enums.Gender.Male

        };

        [TestMethod]
        public async Task GetAllTest()
        {
            var profile = new AutoMapperProfiles();
            var mapConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(mapConfiguration);
            var messages = new MessagesMoq();


            var validator = new ValidatorsMoq();

            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            using (var context = CreateContext(options))
            {
                context.Person.Add(p);

                context.SaveChanges();

                var testResul = new PeopleController(context, mapper, validator,messages);
                IActionResult countResult = await testResul.Index();

                var contentResult = countResult as OkObjectResult;

                var value = contentResult?.Value as List<Person>;
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
            var messages = new MessagesMoq();


            var validator = new ValidatorsMoq();


            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;


            using (var context = CreateContext(options))
            {
                context.Person.Add(p);

                context.SaveChanges();

                var testResul = new PeopleController(context, mapper,validator, messages);
                IActionResult countResult = await testResul.Details(1);

                var contentResult = countResult as OkObjectResult;

                var value = contentResult?.Value as Person;
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
            var messages = new MessagesMoq();

            var validator = new ValidatorsMoq();

            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            using (var context = CreateContext(options))
            {
                var testResul = new PeopleController(context, mapper, validator, messages);
                IActionResult countResult = await testResul.Details(5);


                Assert.IsInstanceOfType(countResult, typeof(Microsoft.AspNetCore.Mvc.NotFoundResult));

            }
        }

        [TestMethod]
        public async Task CreateAllItemsRigth()
        {
            var profile = new AutoMapperProfiles();
            var mapConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(mapConfiguration);
            var messages = new MessagesMoq();

            var validator = new ValidatorsMoq();

            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            using (var context = CreateContext(options))
            {

                var testResul = new PeopleController(context, mapper, validator, messages);
                IActionResult countResult = await testResul.Create(pCreate);

                var contentResult = countResult as OkObjectResult;

                var value = contentResult?.Value as Person;
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
            var messages = new MessagesMoq();


            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            using (var context = CreateContext(options))
            {
                context.Person.Add(p);

                context.SaveChanges();

                var testResul = new PeopleController(context, mapper, validator, messages);
                IActionResult countResult = await testResul.Create(pCreateNull);

                var contentResult = countResult as BadRequestObjectResult;

                Assert.IsInstanceOfType(countResult, typeof(BadRequestObjectResult));
                Assert.IsNotNull(contentResult);
                Assert.AreEqual("Model is null or not valid", contentResult.Value);

            }
        }

        [TestMethod]
        public async Task EditExistingPersonRightValues()
        {
            var profile = new AutoMapperProfiles();
            var mapConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(mapConfiguration);
            var messages = new MessagesMoq();

            var validator = new ValidatorsMoq();

            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            using (var context = CreateContext(options))
            {
                context.Person.Add(p);

                context.SaveChanges();

                var testResul = new PeopleController(context, mapper, validator, messages   );
                IActionResult countResult = await testResul.Edit(1,pEditGood);

                var contentResult = countResult as OkObjectResult;

                var value = contentResult?.Value as Person;
                Assert.IsNotNull(value);
                Assert.AreEqual("NewName", value.FirstName);

            }
        }


        [TestMethod]
        public async Task DeletePerson()
        {
            var profile = new AutoMapperProfiles();
            var mapConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(mapConfiguration);
            var validator = new ValidatorsMoq();
            var messages = new MessagesMoq();


            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            using (var context = CreateContext(options))
            {
                context.Person.Add(p);

                context.SaveChanges();

                var testResul = new PeopleController(context, mapper, validator, messages  );
                IActionResult countResult = await testResul.Delete(1);

                Assert.IsInstanceOfType(countResult, typeof(NoContentResult));


            }
        }
    }
}
