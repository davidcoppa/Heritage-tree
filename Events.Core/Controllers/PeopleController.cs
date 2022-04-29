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
            var data = await context.Person.ToListAsync();
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([Bind("FirstName,SecondName,FirstSurname,SecondSurname,PlaceOfBirth,PlaceOfDeath,DateOfBirth,DateOfDeath,Sex")] PersonCreateDTO personDto)
        {
            if (ModelState.IsValid)
            {
                var person=mapper.Map<Person>(personDto);

                context.Add(person);
                await context.SaveChangesAsync();
               // return RedirectToAction(nameof(Index));
                return Ok(personDto);

            }
            //revesrse map???
            return BadRequest("Model is not valid");
        }

       
        // POST: People/Edit/5
        [HttpPost("Edit/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,SecondName,FirstSurname,SecondSurname,PlaceOfBirth,PlaceOfDeath,DateOfBirth,DateOfDeath,Sex")] Person person)
        {
            if (id != person.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(person);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.ID))
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
    }
}
