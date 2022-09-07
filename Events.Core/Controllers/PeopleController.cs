#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventsManager.Data;
using EventsManager.Model;
using AutoMapper;
using Events.Core.DTOs;
using Events.Core.Common.Validators;
using Events.Core.Common.Queryable;
using System.Linq.Expressions;
using Events.Core.Common.Messages;

namespace Events.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        private readonly EventsContext context;
        private readonly IMapper mapper;
        private readonly IDataValidator validator;
        private readonly IMessages messages;

        private readonly EventController ctrlEvent;
        public PeopleController(EventsContext context,
            IMapper mapper,
            IDataValidator validator,
            IMessages messages
            )
        {
            this.context = context;
            this.mapper = mapper;
            this.validator = validator;
            this.messages = messages;

            this.ctrlEvent = new EventController(context, mapper, validator, messages);
        }

        // GET: People
        [HttpGet("Get")]
        public async Task<IActionResult> Index()
        {
            List<Person> data = await context.Person.ToListAsync();
            return Ok(data);
        }
        // GET: People
        [HttpGet("GetFilter")]
        public async Task<IActionResult> GetFilter([FromQuery] string sort, [FromQuery] string order, [FromQuery] string page, [FromQuery] string itemsPage, [FromQuery] string search)
        {
            try
            {

                IQueryable<Person> data = context.Person.AsQueryable();
                if (search == null)
                {
                    data = context.Person.AsQueryable<Person>().Include(x => x.Photos);
                }
                else
                {
                    data = context.Person.Where(x => x.FirstName.Contains(search)
                                 || x.SecondName.Contains(search)
                                 || x.FirstSurname.Contains(search)
                                 || x.SecondSurname.Contains(search)
                                 //|| x.PlaceOfBirth.stringName.Contains(search)
                                 //|| x.PlaceOfDeath.stringName.Contains(search)
                                 )
                                    .Include(x => x.Photos)
                                    .AsQueryable<Person>();

                }

                if (!data.Any())
                {
                    return Ok(null);
                }
                data = OrderByExtension.OrderBy(data, sort, order);

                int itemsPageInt = int.TryParse(itemsPage, out int items) ? items : Int32.MaxValue;
                Pagination pagination = new Pagination(data.Count(), itemsPageInt);

                int pageIndex = int.TryParse(page, out int count) ? count : 0;

                List<Person> result = data.PagedIndex(pagination, pageIndex).ToList();

                List<PersonWithParents> retVal = await GetParentList(result);

                return Ok(retVal);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.Message);

            }
        }

  //      [HttpGet("GetParentList")]
        private async Task<List<PersonWithParents>> GetParentList(List<Person> lstPersons)
        {
            var evtFilter = await context.Event.Include(a => a.EventType).Where(x => x.EventType.Name == "Nacimiento").ToListAsync();

            var retVal = new List<PersonWithParents>();

            foreach (var person in lstPersons)
            {
                var valEvent = evtFilter.Where(x => x.Person1.Id == person.Id).FirstOrDefault();

                PersonWithParents valToAdd = new PersonWithParents
                {
                    Photos = person.Photos,
                    DateOfBirth = person.DateOfBirth,
                    DateOfDeath = person.DateOfDeath,
                    FirstName = person.FirstName,
                    FirstSurname = person.FirstSurname,
                    Id = person.Id,
                    Order = person.Order,
                    PlaceOfBirth = person.PlaceOfBirth,
                    PlaceOfDeath = person.PlaceOfDeath,
                    SecondName = person.SecondName,
                    SecondSurname = person.SecondSurname,
                    Sex = person.Sex,
                    EventId = (valEvent == null) ? null : valEvent.Id,
                    Father = valEvent?.Person2,
                    Mother = valEvent?.Person3
                };
                retVal.Add(valToAdd);
            }
            return retVal;
        }



        // GET: People/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await context.Person.FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        // POST: People/Create
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(PersonWithParents personDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(messages.BadRequestModelInvalid);
            }

            var person = mapper.Map<Person>(personDto);
            if (validator.ValidateObject<Person>(person))
            {
                return BadRequest(messages.BadRequestModelNullOrInvalid);
            }

            context.Add(person);
            await context.SaveChangesAsync();

            PersonEventBirth peb = new PersonEventBirth
            {
                PersonSon = person,
                PersonFather = personDto.Father,
                PersonMother = personDto.Mother,
                EventId = personDto.EventId
            };
            await ctrlEvent.CreateEventBasedOnNewPerson(peb);

            return Ok(person);
        }


        // POST: People/Edit/5
        [HttpPost("Edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit(int id, PersonWithParents person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(messages.BadRequestModelInvalid);
            }

            var personToEdit = mapper.Map<Person>(person);

            Person entity = await context.Person.FindAsync(id);
            if (entity == null)
            {
                return NotFound(messages.PersonNotFound);
            }

            try
            {
                context.Entry(entity).CurrentValues.SetValues(personToEdit);
                await context.SaveChangesAsync();


                //update the event birth!
                PersonEventBirth peb = new PersonEventBirth
                {
                    PersonSon = personToEdit,
                    PersonFather = person.Father,
                    PersonMother = person.Mother,
                    EventId = person.EventId
                };

                await ctrlEvent.CreateEventBasedOnNewPerson(peb);


            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(personToEdit.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(personToEdit);
        }

        // GET: People/Delete/5
        [HttpPost("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await context.Person.FirstOrDefaultAsync(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            context.Person.Remove(person);
            await context.SaveChangesAsync();
            return NoContent();
        }


        private bool PersonExists(int id)
        {

            return context.Person.Any(e => e.Id == id);
        }


    }
}
