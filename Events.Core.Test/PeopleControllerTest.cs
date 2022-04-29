using Events.Core.Test.Helpers;
using EventsManager.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Core.Test
{
    [TestClass]
    public class PeopleControllerTest:BaseTest
    {
        private static EventsContext CreateContext(DbContextOptions<EventsContext> options)
        {
            return new EventsContext(options, (context, modelBuilder) =>
            {
                modelBuilder.Entity<Files>().ToInMemoryQuery(() =>
                context.File.Select(b => new Files
                {
                    Active = true,
                    Id = 1,
                    Name = "test1",
                    DocumentType = "0",
                    Title = "test",
                    Url = "qwffqw",
                    DateUploaded = DateTime.Now,
                    Description = "asd",
                    Size = 300,
                    WebUrl = "jjjhbjhb"
                }));

            });

        }
    }
}
