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
                modelBuilder.Entity<Person>().ToInMemoryQuery(() =>
                context.Person.Select(b => new Person
                {
                    ID = 1,
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
                    Sex = EventsManager.Enums.Sex.Masculino

                }));

            });

        }
        Person p = new Person
        {
            ID = 1,
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
            Sex = EventsManager.Enums.Sex.Masculino

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
            Sex = EventsManager.Enums.Sex.Masculino

        };
        PersonCreateDTO pCreateNull = new PersonCreateDTO { };


        [TestMethod]
        public async Task GetAllTest()
        {
            var mapper = new Mock<IMapper>();
            var configuration = Configuration;
            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            using (var context = CreateContext(options))
            {
                context.Person.Add(p);

                context.SaveChanges();

                var testResul = new PeopleController(context, mapper.Object);
                IActionResult countResult = await testResul.Index();

                var contentResult = countResult as OkObjectResult;

                var value = contentResult?.Value as List<Person>;
                Assert.IsNotNull(value);
                Assert.AreEqual(1, value.Count);

            }
        }
        PersonEditDTO pEditGood = new PersonEditDTO
        {
            ID = 1,
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
            Sex = EventsManager.Enums.Sex.Masculino

        };

        [TestMethod]
        public async Task GetByIdFound()
        {
            var mapper = new Mock<IMapper>();


            var configuration = Configuration;
            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            using (var context = CreateContext(options))
            {
                context.Person.Add(p);

                context.SaveChanges();

                var testResul = new PeopleController(context, mapper.Object);
                IActionResult countResult = await testResul.Details(1);

                var contentResult = countResult as OkObjectResult;

                var value = contentResult?.Value as Person;
                Assert.IsNotNull(value);
                Assert.AreEqual(1, value.ID);

            }
        }
        [TestMethod]
        public async Task GetByIdNotFound()
        {
            var mapper = new Mock<IMapper>();
            var configuration = Configuration;
            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            using (var context = CreateContext(options))
            {
                context.Person.Add(p);

                context.SaveChanges();

                var testResul = new PeopleController(context, mapper.Object);
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

            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            using (var context = CreateContext(options))
            {

                var testResul = new PeopleController(context, mapper);
                IActionResult countResult = await testResul.Create(pCreate);

                var contentResult = countResult as OkObjectResult;

                var value = contentResult?.Value as Person;
                Assert.IsNotNull(value);
                Assert.AreEqual(1, value.ID);

            }
        }

        [TestMethod]
        public async Task CreateNullObject()
        {
            var profile = new AutoMapperProfiles();
            var mapConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(mapConfiguration);

            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            using (var context = CreateContext(options))
            {
                context.Person.Add(p);

                context.SaveChanges();

                var testResul = new PeopleController(context, mapper);
                IActionResult countResult = await testResul.Create(pCreateNull);

                var contentResult = countResult as BadRequestObjectResult;

                Assert.IsInstanceOfType(countResult, typeof(BadRequestObjectResult));
                Assert.IsNotNull(contentResult);
                Assert.AreEqual("Model is null", contentResult.Value);

            }
        }

        [TestMethod]
        public async Task EditExistingPersonRightValues()
        {
            var profile = new AutoMapperProfiles();
            var mapConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(mapConfiguration);

            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            using (var context = CreateContext(options))
            {
                context.Person.Add(p);

                context.SaveChanges();

                var testResul = new PeopleController(context, mapper);
                IActionResult countResult = await testResul.Edit(1,pEditGood);

                var contentResult = countResult as OkObjectResult;

                var value = contentResult?.Value as Person;
                Assert.IsNotNull(value);
                Assert.AreEqual("NewName", value.FirstName);

            }
        }
    }
}
