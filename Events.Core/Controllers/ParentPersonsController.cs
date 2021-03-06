
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

        // GET: ParentPersons

        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Index()
        {
            var parent = await context.ParentPerson.ToListAsync();
            return Ok(parent);
        }

        // GET: ParentPersons/Details/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parentPerson = await context.ParentPerson.FirstOrDefaultAsync(m => m.Id == id);
            if (parentPerson == null)
            {
                return NotFound();
            }

            return Ok(parentPerson);
        }


        // POST: ParentPersons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create(ParentPersonCreateDTO parentPerson)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(messages.BadRequestModelInvalid);
            }

            ParentPerson person = mapper.Map<ParentPerson>(parentPerson);
            if (validator.ValidateObject<ParentPerson>(person))
            {
                return BadRequest(messages.BadRequestModelInvalid);
            }

            //validate existence of the person if not create (if it's valid)
            if (parentPerson.Person != null)
            {
                Person son = await context.Person.Where(x => x.Id == parentPerson.Person.Id).FirstOrDefaultAsync();
                if (son == null)
                {
                    if (validator.ValidateObject<Person>(parentPerson.Person))
                    {
                        return NotFound("son");
                    }
                    context.Add(parentPerson.Person);
                    son = parentPerson.Person;

                }
                person.Person = son;

            }
            if (parentPerson.PersonMother != null)
            {
                Person mom = await context.Person.Where(x => x.Id == parentPerson.PersonMother.Id).FirstOrDefaultAsync();
                if (mom == null)
                {
                    if (validator.ValidateObject<Person>(parentPerson.PersonMother))
                    {
                        return NotFound("mom");
                    }
                    context.Add(parentPerson.PersonMother);
                    mom = parentPerson.PersonMother;
                }
                person.PersonMother = mom;

            }
            if (parentPerson.PersonFather != null)
            {
                Person dad = await context.Person.Where(x => x.Id == parentPerson.PersonFather.Id).FirstOrDefaultAsync();
                if (dad == null)
                {
                    if (validator.ValidateObject<Person>(parentPerson.PersonFather))
                    {
                        return NotFound("dad");
                    }
                    context.Add(parentPerson.PersonFather);
                    dad = parentPerson.PersonFather;
                }
                person.PersonFather = dad;

            }


            context.Add(person);
            await context.SaveChangesAsync();
            return Ok(person);

        }

        // POST: ParentPersons/Edit/5
        [HttpPost("Edit/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(int id, ParentPersonEditDTO parentPerson)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(messages.BadRequestModelInvalid);
            }

            if (id != parentPerson.Id)
            {
                return NotFound();
            }

            ParentPerson person = mapper.Map<ParentPerson>(parentPerson);
            if (validator.ValidateObject<ParentPerson>(person))
            {
                return BadRequest(messages.BadRequestModelInvalid);
            }

            Person entity = await context.Person.FindAsync(id);
            if (entity == null)
            {
                return NotFound(messages.ParentPersonNotFound);
            }
            try
            {
                context.Entry(entity).CurrentValues.SetValues(person);

                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParentPersonExists(person.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok(person);




        }

        // GET: ParentPersons/Delete/5
        [HttpPost("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parentPerson = await context.ParentPerson.FirstOrDefaultAsync(m => m.Id == id);
            if (parentPerson == null)
            {
                return NotFound();
            }
            context.ParentPerson.Remove(parentPerson);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool ParentPersonExists(int id)
        {
            return context.ParentPerson.Any(e => e.Id == id);
        }
    }
}
