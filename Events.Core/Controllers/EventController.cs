using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventsManager.Data;
using EventsManager.Model;
using Events.Core.DTOs;
using AutoMapper;

namespace Events.Core.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        private readonly EventsContext context;
        private readonly IMapper mapper;


        public EventController(EventsContext context,
            IMapper mapper
            )
        {
            this.context = context;
            this.mapper = mapper;

        }

        // GET: Events
        [HttpGet("Get")]
        public async Task<IActionResult> Index()
        {
            var events = await context.Event.ToListAsync();
            return Ok(events);
        }


        // GET: Events/Details/5
        [HttpGet("Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await context.Event.FirstOrDefaultAsync(m => m.ID == id);
            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

       
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([Bind("Title,Description,EventDate")] EventCreateDTO @event)
        {
            
            if (ModelState.IsValid)
            {
                Event evento =mapper.Map<Event>(@event);

                context.Add(evento);
                await context.SaveChangesAsync();
                return Ok(evento);
            }
            return BadRequest("Model is not valid");
        }

       

        // POST: Events/Edit/5
        [HttpPost("Edit/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,EventDate")] Event @event)
        {
            if (id != @event.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(@event);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(@event);
            }
            return BadRequest("Model is not valid");
        }

        // GET: Events/Delete/5
        [HttpPost("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await context.Event.FirstOrDefaultAsync(m => m.ID == id);
            if (@event == null)
            {
                return NotFound();
            }
            context.Event.Remove(@event);
            await context.SaveChangesAsync();

            return NoContent();
        }

        private bool EventExists(int id)
        {
            return context.Event.Any(e => e.ID == id);
        }
    }
}
