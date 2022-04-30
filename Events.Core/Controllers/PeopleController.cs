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

namespace Events.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PeopleController : Controller
    {
        private readonly EventsContext context;
        private readonly IMapper mapper;

        public PeopleController(EventsContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;

        }

        // GET: People
        [HttpGet("Get")]
        public async Task<IActionResult> Index()
        {
            List<Person> data = await context.Person.ToListAsync();
            return Ok(data);
        }

        // GET: People/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await context.Person.FirstOrDefaultAsync(m => m.ID == id);
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
                if (!validateObject(person))
                {
                    context.Add(person);
                    await context.SaveChangesAsync();
                    return Ok(person);
                }

                return BadRequest("Model is null");



            }
            return BadRequest("Model is not valid");
        }


        // POST: People/Edit/5
        [HttpPost("Edit/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit(int id, PersonEditDTO person)
        {
            if (id != person.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var personToEdit = mapper.Map<Person>(person);

                //TODO: data validation

                var entity = await context.Person.FindAsync(id);
                if (entity == null)
                {
                    return NotFound();
                }

                try
                {
                    context.Entry(entity).CurrentValues.SetValues(personToEdit);

                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(personToEdit.ID))
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
            return BadRequest("Model is not valid");
        }

        // GET: People/Delete/5
        [HttpPost("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await context.Person.FirstOrDefaultAsync(m => m.ID == id);
            if (person == null)
            {
                return NotFound();
            }
            context.Person.Remove(person);
            await context.SaveChangesAsync();
            return Ok();
        }


        private bool PersonExists(int id)
        {

            return context.Person.Any(e => e.ID == id);
        }

        private bool validateObject(Person myObject)
        {
            return myObject.GetType().GetProperties()
            .Where(p => p.GetValue(myObject) is string) // selecting only string props
            .All(p => string.IsNullOrWhiteSpace((p.GetValue(myObject) as string)));

            //return myObject.GetType()
            //    .GetProperties() //get all properties on object
            //    .Select(pi => pi.GetValue(myObject)) //get value for the property
            //    .Any(value => value != null); // Check if one of the values is not null, if so it returns true.

        }
    }
}
