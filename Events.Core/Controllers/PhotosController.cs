using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventsManager.Data;
using EventsManager.Model;

namespace Events.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhotosController : Controller
    {
        private readonly EventsContext context;

        public PhotosController(EventsContext context)
        {
            this.context = context;

        }

        // GET: Photos
        [HttpGet("Get")]
        public async Task<IActionResult> Index()
        {
            var photos = await context.Photos.ToListAsync();
            return Ok(photos);
        }

        // GET: Photos/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photos = await context.Photos.FirstOrDefaultAsync(m => m.Id == id);
            if (photos == null)
            {
                return NotFound();
            }

            return Ok(photos);
        }

        // POST: Photos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([Bind("Date,Description,UrlFile")] Photos photos)
        {
            if (ModelState.IsValid)
            {
                context.Add(photos);
                await context.SaveChangesAsync();
                return Ok(photos);

            }
            return BadRequest("Model is not valid");
        }

       
        // POST: Photos/Edit/5
   
        [HttpPost("Edit/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Description")] Photos photos)
        {
            if (id != photos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(photos);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotosExists(photos.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(photos);
            }
            return BadRequest("Model is not valid");
        }

        // GET: Photos/Delete/5
        [HttpPost("Delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photos = await context.Photos.FirstOrDefaultAsync(m => m.Id == id);
            if (photos == null)
            {
                return NotFound();
            }
            context.Photos.Remove(photos);
            await context.SaveChangesAsync();
            return NoContent();
        }

       

        private bool PhotosExists(int id)
        {
            return context.Photos.Any(e => e.Id == id);
        }
    }
}
