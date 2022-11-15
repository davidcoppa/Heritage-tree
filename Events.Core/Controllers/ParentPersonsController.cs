
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventsManager.Data;
using EventsManager.Model;
using Events.Core.DTOs;
using AutoMapper;
using Events.Core.Common.Validators;
using Events.Core.Common.Messages;

namespace Events.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParentPersonsController : Controller
    {
        private readonly EventsContext context;
        private readonly IMapper mapper;
        private readonly IDataValidator validator;
        private readonly IMessages messages;


        public ParentPersonsController(EventsContext context,
            IMapper mapper,
            IDataValidator validator,
            IMessages messages)
        {
            this.context = context;
            this.mapper = mapper;
            this.validator = validator;
            this.messages = messages;
        }

        //[HttpGet("GetAllFilter2")]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetAllFilter2(int? idPerson)
        //{
        //    if (idPerson == null)
        //    {
        //        return NotFound();
        //    }

        //    Person personSon = await context.Person.FirstOrDefaultAsync(m => m.Id == idPerson);
        //    //  Person personSon = GetPersonAsync(idPerson??-1);

        //    if (personSon == null)
        //    {
        //        return NotFound();
        //    }
        //    var eventsList = await GetEventListPersonAsync(personSon);

        //    if (eventsList == null)
        //    {
        //        return NotFound("son");
        //    }

        //    PersonWithParentsDTO pwp = CreatePersonWithParent(personSon);

        //    if (eventsList.Person2 != null)
        //    {
        //        Event eventListPerson = await GetEventListPersonAsync(eventsList.Person2);
        //        PersonWithParentsDTO pwp2 = CreatePersonWithParent(eventsList.Person2);
        //        pwp.children.Add(pwp2);
        //    }
        //    if (eventsList.Person3 != null)
        //    {
        //        Event eventListPerson = await GetEventListPersonAsync(eventsList.Person3);
        //        PersonWithParentsDTO pwp3 = CreatePersonWithParent(eventsList.Person3);
        //        pwp.children.Add(pwp3);
        //    }

        //    return Ok(pwp);
        //}

        [HttpGet("GetAllFilter")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllFilter(int idPerson)
        {
            if (idPerson == 0)
            {
                return NotFound();
            }

            var person = await context.Person.FirstOrDefaultAsync(m => m.Id == idPerson);
            if (person == null)
            {
                return NotFound();
            }
            var events = await context.Event.Where(m => m.Description == "Nacimiento"
                                                              && (m.Person1.Id.Equals(idPerson)
                                                                //|| m.Person2.Id.Equals(idPerson)
                                                                //|| m.Person3.Id.Equals(idPerson)
                                                                ))
                                                      .Include(x => x.Person1)
                                                      .Include(x => x.Person2)
                                                      .Include(x => x.Person3)
                                                      .FirstOrDefaultAsync();

            if (events == null)
            {
                return NotFound("event person");
            }

            var retValue = new RetValue
            {
                Name = person.FirstName ?? "No name",
                Value = person.Order ?? 1,
                children = new List<RetValue>()
            };

            //get sons




            //get parents
            if (events.Person2 != null)
            {
                Person personFather = await context.Person.FirstOrDefaultAsync(m => m.Id == events.Person2.Id);

                var ss = await GetParents(personFather);
                retValue.children.Add(ss);

            }
            if (events.Person3 != null)
            {
                Person personMother = await context.Person.FirstOrDefaultAsync(m => m.Id == events.Person3.Id);

                var ss = await GetParents(personMother);
                retValue.children.Add(ss);

            }



            return Ok(retValue);
        }


        private async Task<RetValue> GetParents(Person p)
        {
            var retValue = new RetValue();

            if (p == null)
            {
                return retValue;
            }

            var events = await context.Event.Where(m => m.Description == "Nacimiento"
                                                                && (m.Person1.Id.Equals(p.Id)
                                                                  ))
                                                        .Include(x => x.Person1)
                                                        .Include(x => x.Person2)
                                                        .Include(x => x.Person3)
                                                        .FirstOrDefaultAsync();

            if (events == null)
            {
                return retValue;
                
            }

            retValue.Name = events.Person1.FirstName ?? "No name";
            retValue.Value = events.Person1.Order ?? 1;
            retValue.children = new List<RetValue>();

            if (events.Person2 != null)
            {
                Person personFather = await context.Person.FirstOrDefaultAsync(m => m.Id == events.Person2.Id);

                var ss = await GetParents(personFather);
                retValue.children.Add(ss);

            }
            if (events.Person3 != null)
            {
                Person personMother = await context.Person.FirstOrDefaultAsync(m => m.Id == events.Person3.Id);

                var ss = await GetParents(personMother);
                retValue.children.Add(ss);

            }

            return retValue;
        }

    }
}
