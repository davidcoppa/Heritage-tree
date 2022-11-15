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
    public class ParentPersonsControllerTest : BaseTest
    {
        private static EventsContext CreateContext(DbContextOptions<EventsContext> options)
        {
            return new EventsContext(options, (context, modelBuilder) =>
            {
                modelBuilder.Entity<ParentPerson>();
                modelBuilder.Entity<Person>();
            });
        }

        ParentPerson pp = new ParentPerson
        {
            Id = 1,
            Description = "Son",
            Person = new Person { Id = 1, FirstName = "pepe" },
            PersonMother = new Person { Id = 2, FirstName = "ana" },
            PersonFather = new Person { Id = 3, FirstName = "juan" }

        };
        Person pSon = new() { Id = 1, FirstName = "pepe" };
        Person pMother = new() { Id = 2, FirstName = "ana" };
        Person pFather = new() { Id = 3, FirstName = "juan" };

        ParentPersonCreateDTO ppCreate = new ParentPersonCreateDTO
        {
            Description = "Son",
            Person = new Person { Id = 1, FirstName = "pepe" },
            PersonMother = new Person { Id = 2, FirstName = "ana" },
            PersonFather = new Person { Id = 3, FirstName = "juan" }

        };
        ParentPersonCreateDTO ppCreateNull = new() { };

        ParentPersonCreateDTO ppCreateNoMother = new()
        {
            Description = "Son",
            Person = new Person { Id = 1 },
            PersonMother = new Person {FirstName = "ana" },
            PersonFather = new Person { Id = 3, FirstName = "juan" }
        };

        ParentPersonCreateDTO ppCreateNoSon = new()
        {
            Description = "Son",
            Person = new Person { Id = 1},//no name, coudnt create the person
            PersonMother = new Person { Id = 2, FirstName = "ana" },
            PersonFather = new Person { Id = 3, FirstName = "juan" }
        };

        ParentPersonEditDTO ppeDto = new()
        {
            Id = 1,
            Description = "NewSon",
            Person = new Person { Id = 1,FirstName="son" },
            PersonMother = new Person { FirstName = "ana" },
            PersonFather = new Person { Id = 3, FirstName = "juan" }
        };


        [TestMethod]
        public async Task GetAllTest()
        {
            var validator = new ValidatorsMoq();

            var profile = new AutoMapperProfiles();
            var mapConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(mapConfiguration);
            var messages = new MessagesMoq();


            var options = new DbContextOptionsBuilder<EventsContext>()
             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
             .Options;

            using (var context = CreateContext(options))
            {
                context.ParentPerson.Add(pp);

                context.SaveChanges();

                var testResul = new ParentPersonsController(context, mapper, validator,messages);
                IActionResult countResult = await testResul.Index();

                var contentResult = countResult as OkObjectResult;

                var value = contentResult?.Value as List<ParentPerson>;
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
                context.ParentPerson.Add(pp);

                context.SaveChanges();

                var testResul = new ParentPersonsController(context, mapper, validator, messages);
                IActionResult countResult = await testResul.GetAllFilter(1);

                var contentResult = countResult as OkObjectResult;

                var value = contentResult?.Value as ParentPerson;
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
                var testResul = new ParentPersonsController(context, mapper, validator, messages);
                IActionResult countResult = await testResul.GetAllFilter(5);


                Assert.IsInstanceOfType(countResult, typeof(NotFoundResult));

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

                context.Person.Add(pSon);
                context.Person.Add(pMother);
                context.Person.Add(pFather);
                context.SaveChanges();

                var testResul = new ParentPersonsController(context, mapper, validator, messages);
                IActionResult countResult = await testResul.Create(ppCreate);

                var contentResult = countResult as OkObjectResult;

                var value = contentResult?.Value as ParentPerson;
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
                context.SaveChanges();

                var testResul = new ParentPersonsController(context, mapper, validator, messages);
                IActionResult countResult = await testResul.Create(ppCreateNull);

                var contentResult = countResult as BadRequestObjectResult;

                Assert.IsInstanceOfType(countResult, typeof(BadRequestObjectResult));
                Assert.IsNotNull(contentResult);
                Assert.AreEqual("Model is not valid", contentResult.Value);

            }
        }

        [TestMethod]
        public async Task CreateNullMother()
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
                context.Person.Add(pSon);
                context.Person.Add(pFather);
                context.SaveChanges();

                var testResul = new ParentPersonsController(context, mapper, validator, messages);
                IActionResult countResult = await testResul.Create(ppCreateNoMother);

                var contentResult = countResult as OkObjectResult;

                var value = contentResult?.Value as ParentPerson;
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
            var messages = new MessagesMoq();

            var validator = new ValidatorsMoq();

            var options = new DbContextOptionsBuilder<EventsContext>()
              .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
              .Options;

            using (var context = CreateContext(options))
            {
                context.Person.Add(pFather);
                context.Person.Add(pMother);
                context.SaveChanges();

                var testResul = new ParentPersonsController(context, mapper, validator, messages);
                IActionResult countResult = await testResul.Create(ppCreateNoSon);

                Assert.IsInstanceOfType(countResult, typeof(NotFoundObjectResult));


            }
        }

        [TestMethod]
        public async Task EditExistingParentPersonRightValues()
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
                context.ParentPerson.Add(pp);

                context.SaveChanges();

                var testResul = new ParentPersonsController(context, mapper, validator, messages);
                IActionResult countResult = await testResul.Edit(1, ppeDto);

                var contentResult = countResult as OkObjectResult;

                var value = contentResult?.Value as ParentPerson;
                Assert.IsNotNull(value);
                Assert.AreEqual("NewSon", value.Description);

            }
        }

        [TestMethod]
        public async Task DeletePerson()
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
                context.ParentPerson.Add(pp);

                context.SaveChanges();

                var testResul = new ParentPersonsController(context, mapper, validator, messages);
                IActionResult countResult = await testResul.Delete(1);

                Assert.IsInstanceOfType(countResult, typeof(NoContentResult));


            }
        }


    }
}
