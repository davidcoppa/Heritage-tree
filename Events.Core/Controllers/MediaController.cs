using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventsManager.Data;
using EventsManager.Model;
using Events.Core.Common.Messages;
using Events.Core.Common.Queryable;
using Events.Core.DTOs;
using AutoMapper;
using Events.Core.Model;

namespace Events.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : Controller
    {
        private readonly EventsContext context;
        private readonly IMessages messages;
        private readonly IMapper mapper;

        public MediaController(EventsContext context,
            IMessages messages,
            IMapper mapper)
        {
            this.context = context;
            this.messages = messages;
            this.mapper = mapper;
        }

        //// GET: Photos
        //[HttpGet("Get")]
        //public async Task<IActionResult> Index()
        //{
        //    var photos = await context.Media.ToListAsync();
        //    return Ok(photos);
        //}

        //// GET: Photos/Details/5
        //[HttpGet]
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var photos = await context.Media.FirstOrDefaultAsync(m => m.Id == id);
        //    if (photos == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(photos);
        //}

        [HttpGet("GetFilter")]
        public async Task<IActionResult> GetFilter([FromQuery] string sort, [FromQuery] string order, [FromQuery] string page, [FromQuery] string itemsPage, [FromQuery] string? search)
        {
            try
            {

                IQueryable<Media> data = context.Media.AsQueryable();
                if (search == null)
                {
                    data = context.Media.AsQueryable<Media>().Include(x => x.Event).Include(x => x.File);
                }
                else
                {
                    data = context.Media.Where(x => x.Name.Contains(search)
                                 || x.Description.Contains(search)
                                 || x.MediaDateUploaded.ToString().Contains(search))
                                        .Include(x => x.Event)
                                        .Include(x => x.File)
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
        public async Task<IActionResult> Create(MediaCreateDTO photos)
        {
            try
            {


                if (!ModelState.IsValid)
                {
                    return BadRequest(messages.BadRequestModelInvalid);

                }
                Media evt = mapper.Map<Media>(photos);



                var @event = await context.Event.Where(m => m.Id == photos.Event.Id).FirstOrDefaultAsync();

                if (@event == null)
                {
                    context.Add(photos.Event);
                }
                evt.Event = @event;



                if (evt.File!=null)
                {
                    foreach (var item in evt.File)
                    {

                        MediaType mediatype = await context.MediaType.Where(m => m.Id == item.DocumentType.Id).FirstOrDefaultAsync();
                        if (mediatype == null)
                        {
                            item.DocumentType = mediatype;

                            context.Add(item.DocumentType);
                        }
                        //   MediaTypeDTO mt = mapper.Map<MediaTypeDTO>(mediatype);
                        item.DocumentType = mediatype;
                        
                        context.FileData.Add(item);
                        await context.SaveChangesAsync();

                        //    context.Entry(item.DocumentType).CurrentValues.SetValues(item.DocumentType);

                        //    item.DocumentType= mediatype;

                    }
                }
                context.Media.Add(evt);



                //if (photos.TagItems != null)
                //{
                //    List<Tags> tagList = await context.Tags.ToListAsync();
                //    List<MediaTags> mediaTags = new List<MediaTags>();
                //    foreach (var tag in photos.TagItems)
                //    {
                //        //delete maediaTag
                //        //if (!tag.Active)
                //        //{

                //        //}
                //        //new tag
                //        if (tag.Id == -1)
                //        {
                //            var newTag = new Tags { Name = tag.Name };
                //            mediaTags.Add(new MediaTags { Tags = newTag, Media = evt });
                //      //      context.Tags.Add(newTag);
                //        }
                //        else //editTag
                //        {
                //            var pp = tagList.Where(x => x.Id == tag.Id);

                //        }



                //    }
                //}



                await context.SaveChangesAsync();


                return Ok(photos);
            }
            catch (Exception ex)
            {

                throw;
            }

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
