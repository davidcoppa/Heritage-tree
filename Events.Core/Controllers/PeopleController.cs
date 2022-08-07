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

        public PeopleController(EventsContext context,
            IMapper mapper,
            IDataValidator validator,
            IMessages messages)
        {
            this.context = context;
            this.mapper = mapper;
            this.validator = validator;
            this.messages = messages;

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
                    data = context.Person.AsQueryable<Person>();
                }
                else
                {
                    data = context.Person.Where(x => x.FirstName.Contains(search)
                                 || x.SecondName.Contains(search)
                                 || x.FirstSurname.Contains(search)
                                 || x.SecondSurname.Contains(search)
                                 || x.PlaceOfBirth.Contains(search)
                                 || x.PlaceOfDeath.Contains(search)).AsQueryable<Person>();

                }

                if (!data.Any())
                {
                    return Ok(null);
                }
                data = OrderByExtension.OrderBy(data, sort, order);

                int itemsPageInt = int.TryParse(itemsPage, out int items) ? items : Int32.MaxValue;
                Pagination pagination = new Pagination(data.Count(), itemsPageInt);

                int pageIndex = int.TryParse(page, out int count) ? count : 0;

                var result = data.PagedIndex(pagination, pageIndex).ToList();

                return Ok(data);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.Message);

            }
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
        public async Task<IActionResult> Create(PersonCreateDTO personDto)
        {
            if (ModelState.IsValid)
            {
                var person = mapper.Map<Person>(personDto);
                if (validator.ValidateObject<Person>(person))
                {
                    return BadRequest(messages.BadRequestModelNullOrInvalid);
                }
                context.Add(person);
                await context.SaveChangesAsync();
                return Ok(person);
            }
            return BadRequest(messages.BadRequestModelInvalid);
        }


        // POST: People/Edit/5
        [HttpPost("Edit")]
        //[HttpPost("Edit/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit(int id, PersonEditDTO person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
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
            return BadRequest(messages.BadRequestModelInvalid);
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
