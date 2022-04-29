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
using Events.Core.DTOs;
using AutoMapper;

namespace Events.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ParentPersonsController : Controller
    {
        private readonly EventsContext context;
        private readonly IMapper mapper;

        public ParentPersonsController(EventsContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
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

            var parentPerson = await context.ParentPerson.FirstOrDefaultAsync(m => m.ID == id);
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
        public async Task<IActionResult> Create([Bind("Description,PersonFather,PersonMother,Person")] ParentPersonCreateDTO parentPerson)
        {
            if (ModelState.IsValid)
            {
                ParentPerson person = mapper.Map<ParentPerson>(parentPerson);

                //validate existence of the person
                if (parentPerson.PersonMother != null)
                {
                    Person son = await context.Person.Where(x => x.ID == parentPerson.Person.ID).FirstOrDefaultAsync();
                    if (son == null)
                    {
                        return NotFound("son");

                    }
                    person.Person = son;

                }
                if (parentPerson.PersonMother != null)
                {
                    Person mom = await context.Person.Where(x => x.ID == parentPerson.PersonMother.ID).FirstOrDefaultAsync();
                    if (mom == null)
                    {
                        return NotFound("mom");

                    }
                    person.PersonMother = mom;

                }
                if (parentPerson.PersonMother != null)
                {
                    Person dad = await context.Person.Where(x => x.ID == parentPerson.PersonFather.ID).FirstOrDefaultAsync();
                    if (dad == null)
                    {
                        return NotFound("dad");

                    }
                    person.PersonFather = dad;

                }


                context.Add(person);
                await context.SaveChangesAsync();
                return Ok(person);
            }
            return BadRequest("Model is not valid");
        }

        // POST: ParentPersons/Edit/5
        [HttpPost("Edit/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Description,PersonFather,PersonMother,Person")] ParentPerson parentPerson)
        {
            if (id != parentPerson.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(parentPerson);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParentPersonExists(parentPerson.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(parentPerson);
            }
            return BadRequest("Model is not valid");
        }

        // GET: ParentPersons/Delete/5
        [HttpPost("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parentPerson = await context.ParentPerson.FirstOrDefaultAsync(m => m.ID == id);
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
            return context.ParentPerson.Any(e => e.ID == id);
        }
    }
}
