#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Events.Core.Model;
using EventsManager.Data;

namespace Events.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationsController : Controller
    {
        private readonly EventsContext context;

        public LocationsController(EventsContext context)
        {
            this.context = context;
        }

        // GET: Locations
        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Index()
        {
            var location = await context.Location.ToListAsync();
            return Ok(location);
        }

        // GET: Locations/Details/5
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await context.Location.FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

 

        // POST: Locations/Create
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([Bind("Country,City,lat,lgn")] Location location)
        {
            if (ModelState.IsValid)
            {
                context.Add(location);
                await context.SaveChangesAsync();

                return Ok(location);
            }
            return BadRequest("Model is not valid");
        }

   

        // POST: Locations/Edit/5
        [HttpPost("Edit/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Country,City,lat,lgn")] Location location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(location);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(location);
            }
            return BadRequest("Model is not valid");
        }

        // GET: Locations/Delete/5
        [HttpPost("Delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await context.Location.FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            context.Location.Remove(location);
            await context.SaveChangesAsync();
            return NoContent();
        }
        private bool LocationExists(int id)
        {
            return context.Location.Any(e => e.Id == id);
        }
    }
}
