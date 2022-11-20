using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventsManager.Data;
using EventsManager.Model;
using Events.Core.Common.Messages;
using Events.Core.Common.Queryable;

namespace Events.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : Controller
    {
        private readonly EventsContext context;
        private readonly IMessages messages;

        public MediaController(EventsContext context,
            IMessages messages)
        {
            this.context = context;
            this.messages = messages;
        }

        // GET: Photos
        [HttpGet("Get")]
        public async Task<IActionResult> Index()
        {
            var photos = await context.Media.ToListAsync();
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

            var photos = await context.Media.FirstOrDefaultAsync(m => m.Id == id);
            if (photos == null)
            {
                return NotFound();
            }

            return Ok(photos);
        }

        [HttpGet("GetFilter")]
        public async Task<IActionResult> GetFilter([FromQuery] string sort, [FromQuery] string order, [FromQuery] string page, [FromQuery] string itemsPage, [FromQuery] string? search)
        {
            try
            {

                IQueryable<Media> data = context.Media.AsQueryable();
                if (search == null)
                {
                    data = context.Media.AsQueryable<Media>().Include(x => x.MediaType);
                }
                else
                {
                    data = context.Media.Where(x => x.Name.Contains(search)
                                 || x.Description.Contains(search)
                                 || x.MediaDate.ToString().Contains(search)
                                 || x.MediaDateUploaded.ToString().Contains(search)
                                 || x.MediaType.Name.Contains(search)
                                 || x.MediaType.Description.Contains(search))
                                        .Include(x => x.MediaType)
                                        .AsQueryable<Media>();

                }

                if (!data.Any())
                {
                    return Ok(null);
                }
                data = OrderByExtension.OrderBy(data, sort, order);

                int itemsPageInt = int.TryParse(itemsPage, out int items) ? Int32.MaxValue : items;
                Pagination pagination = new Pagination(data.Count(), itemsPageInt);

                int pageIndex = int.TryParse(page, out int count) ? 0 : count;

                List<Media>? result = data.PagedIndex(pagination, pageIndex).ToList();

                return Ok(result);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.Message);

            }
        }


        // POST: Photos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([Bind("Date,Description,UrlFile")] Media photos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(messages.BadRequestModelInvalid);

            }
            context.Add(photos);
            await context.SaveChangesAsync();
            return Ok(photos);


        }


        // POST: Photos/Edit/5

        [HttpPost("Edit/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Description")] Media photos)
        {
          
            if (!ModelState.IsValid)
            {
                return BadRequest(messages.BadRequestModelInvalid);
            }

            if (id != photos.Id)
            {
                return NotFound();
            }


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

            var photos = await context.Media.FirstOrDefaultAsync(m => m.Id == id);
            if (photos == null)
            {
                return NotFound();
            }
            context.Media.Remove(photos);
            await context.SaveChangesAsync();
            return NoContent();
        }



        private bool PhotosExists(int id)
        {
            return context.Media.Any(e => e.Id == id);
        }
    }
}
