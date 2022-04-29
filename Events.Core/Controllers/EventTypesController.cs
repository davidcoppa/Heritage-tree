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

namespace Events.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventTypesController : Controller
    {
        private readonly EventsContext context;

        public EventTypesController(EventsContext context)
        {
            this.context = context;
        }

        // GET: EventTypes
        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Index()
        {
            var events = await context.EventType.ToListAsync();
            return Ok(events);
        }


    

        // GET: EventTypes/Details/5
        [HttpGet("Get/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventTypes = await context.EventType.FirstOrDefaultAsync(m => m.Id == id);
            if (eventTypes == null)
            {
                return NotFound();
            }

            return Ok(eventTypes);
        }

      
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([Bind("Name,Description")] EventTypes eventTypes)
        {
            if (ModelState.IsValid)
            {
                context.Add(eventTypes);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(eventTypes);
        }


        [HttpPost("Edit/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description")] EventTypes eventTypes)
        {
            if (id != eventTypes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(eventTypes);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventTypesExists(eventTypes.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(eventTypes);
            }
            return BadRequest("Model is not valid");
        }

        // GET: EventTypes/Delete/5
        [HttpPost("Delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventTypes = await context.EventType.FirstOrDefaultAsync(m => m.Id == id);
            if (eventTypes == null)
            {
                return NotFound();
            }

            context.EventType.Remove(eventTypes);
            await context.SaveChangesAsync();
            return NoContent();
        }

        private bool EventTypesExists(int id)
        {
            return context.EventType.Any(e => e.Id == id);
        }
    }
}
