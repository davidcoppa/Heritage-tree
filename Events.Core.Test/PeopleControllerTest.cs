using Events.Core.Test.Helpers;
using EventsManager.Data;
using EventsManager.Model;
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
    }
}
