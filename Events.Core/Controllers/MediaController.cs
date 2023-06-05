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
using Events.core.Common.Files;

namespace Events.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MediaController : Controller
    {
        private readonly EventsContext context;
        private readonly IMessages messages;
        private readonly IMapper mapper;
        private readonly IFileManager fileManager;

        public MediaController(EventsContext context, IMessages messages, IMapper mapper, IFileManager fileManager)
        {
            this.context = context;
            this.messages = messages;
            this.mapper = mapper;
            this.fileManager = fileManager;
        }


        [HttpGet("GetFilter")]
        public async Task<IActionResult> GetFilter([FromQuery] string sort, [FromQuery] string order, [FromQuery] string page, [FromQuery] string itemsPage, [FromQuery] string? search)
        {
            try
            {

                IQueryable<Media> data = context.Media.AsQueryable();
                if (search == null)
                {
                    data = context.Media.AsQueryable<Media>().Include(x => x.Event).Include(x => x.File).Include(x => x.TagItems);
                }
                else
                {
                    data = context.Media.Where(x => x.Name.Contains(search)
                                 || x.Description.Contains(search)
                                 || x.DateUploaded.ToString().Contains(search))
                                         .Include(x => x.Event)
                                        .Include(x => x.File)
                                        .Include(x => x.TagItems)
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

                List<Media> result = data.PagedIndex(pagination, pageIndex).ToList();

                List<MediaDTO> rtn = mapper.Map<List<MediaDTO>>(result);

                foreach (MediaDTO mediaFile in rtn)
                {


                    if (mediaFile.File == null)
                    {
                        continue;
                    }

                    mediaFile.OnlyFilesInfo = mediaFile.File;

                    foreach (var tmbFile in mediaFile.File)
                    {
                        if (tmbFile.Url == null)
                        {
                            continue;
                        }

                        if (tmbFile.UrlPreview != null)
                        {
                            continue;
                        }


                        if (fileManager.FileExists(tmbFile.Url))
                        {
                            try
                            {
                                tmbFile.UrlPreview = fileManager.WriteImageThumbnail(tmbFile.Url);
                                //context.Entry(pp).CurrentValues.SetValues(pp);
                                var pp = await context.FileData.Where(x => x.Id == tmbFile.Id).FirstOrDefaultAsync();
                                if (pp == null)
                                {
                                    //throw error
                                    throw new ApplicationException("the file doesn't exist on the database");
                                }
                                pp.UrlPreview = tmbFile.UrlPreview;
                                context.Update(pp);

                                await context.SaveChangesAsync();

                            }
                            catch (Exception ex)
                            {
                                //TODO: log error
                                throw;
                            }
                        }

                    }

                }



                return Ok(rtn);


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



                var @event = await context.Event.Where(m => m.Id == photos.Event.ID).FirstOrDefaultAsync();

                if (@event == null)
                {
                    context.Add(photos.Event);
                }
                evt.Event = @event;



                if (evt.File != null)
                {
                    foreach (var item in evt.File)
                    {
                        item.UrlPreview = fileManager.ChangePathNameNoExtension(item.Url, "-small");

                        MediaType mediatype = await context.MediaType.Where(m => m.Id == item.DocumentType.Id).FirstOrDefaultAsync();
                        if (mediatype == null)
                        {
                            item.DocumentType = mediatype;

                            context.Add(item.DocumentType);
                        }
                        //   MediaTypeDTO mt = mapper.Map<MediaTypeDTO>(mediatype);
                        item.DocumentType = mediatype;

                        context.Entry(item.DocumentType).CurrentValues.SetValues(item.DocumentType);
                        //context.FileData.Add(item);
                        //await context.SaveChangesAsync();

                        //    item.DocumentType= mediatype;

                    }

                }



                if (photos.TagItems != null)
                {
                    List<Tags> tagList = await context.Tags.ToListAsync();
                    foreach (var tag in photos.TagItems)
                    {
                        Tags pp = tagList.Where(x => x.Name == tag.Name).FirstOrDefault();
                        if (pp != null)
                        {
                            context.Entry(pp).CurrentValues.SetValues(pp);

                        }
                        else
                        {
                            var newTag = new Tags { Name = tag.Name };
                            context.Add(newTag);
                        }

                    }
                }

                context.Media.Add(evt);


                await context.SaveChangesAsync();


                return Ok(photos);
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        // POST: Photos/Edit/5

        [HttpPost("Edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit(int id, MediaDTO photos)
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
                Media mediaOnDb = await context.Media.Where(x => x.Id == photos.Id)
                                                   .Include(x => x.File)
                                                   .Include(x => x.Event)
                                                   .Include(x => x.TagItems)
                                                   .FirstOrDefaultAsync();

                if (mediaOnDb == null)
                {
                    return BadRequest(messages.BadRequestModelInvalid);
                }

                //get the event
                if (photos.Event != null && mediaOnDb.Event != null)
                {
                    //the main event of the media changed, so we update it
                    if (photos.Event.ID != mediaOnDb.Event.Id)
                    {

                        Event? newEvt = await context.Event.Where(x => x.Id == photos.Event.ID).Include(x => x.Person1)
                                                                     .Include(x => x.Person2)
                                                                     .Include(x => x.Person3)
                                                                     .Include(x => x.EventType)
                                                                     .FirstOrDefaultAsync();
                        if (newEvt != null)
                        {
                            mediaOnDb.Event = newEvt;
                            context.Entry(mediaOnDb.Event).CurrentValues.SetValues(newEvt);

                        }
                    }
                }

                //get the tags and add the new ones
                if (photos.TagItems != null)
                {
                    List<Tags> listTagsToAdd = new List<Tags>();
                    if (mediaOnDb.TagItems==null||mediaOnDb.TagItems.Count == 0)
                    {
                        List<TagsEditDTO> listnewtags = photos.TagItems;
                        //add the new tags
                        foreach (var item in listnewtags)
                        {
                            var editTag = mapper.Map<TagsDTO>(item);
                            var tagToBd= mapper.Map<Tags>(editTag);
                            listTagsToAdd.Add(tagToBd);
                        }
                        mediaOnDb.TagItems = listTagsToAdd;
                    }
                    else
                    {
                        //compare tags and add/remove
                        foreach (var item in photos.TagItems)
                        {
                            var existTag = await context.Tags.Where(x => x.Id == item.Id).FirstOrDefaultAsync();

                            //new one
                            if (existTag == null)
                            {
                                var editTag = mapper.Map<TagsDTO>(item);
                                var tagToBd = mapper.Map<Tags>(editTag);
                                listTagsToAdd.Add(tagToBd);
                            }
                            else
                            {
                                //existing 
                                var editTag = mapper.Map<TagsDTO>(item);
                                var tagToBd = mapper.Map<Tags>(editTag);
                                listTagsToAdd.Add(tagToBd);
                            }
                        }
                        mediaOnDb.TagItems = listTagsToAdd;

                    }
                }

                //get the files and add the new ones
                if (photos.File != null)
                {
                    List<FileData> listFileToAdd = new List<FileData>();
                    if (mediaOnDb.File == null || mediaOnDb.File.Count == 0)
                    {
                        List<FileDataDTO> listnewFiles = photos.File;
                        //add the new tags
                        foreach (var item in listnewFiles)
                        {
                            var editFile = mapper.Map<FileData>(item);
                            //   var fileToBd = mapper.Map<Tags>(editFile);
                            listFileToAdd.Add(editFile);
                        }
                        mediaOnDb.File = listFileToAdd;
                    }
                    else
                    {
                        //compare tags and add/remove
                        foreach (var item in photos.File)
                        {
                            var existFile = await context.FileData.Where(x => x.Id == item.Id).FirstOrDefaultAsync();

                            //new one
                            if (existFile == null)
                            {
                                item.UrlPreview = fileManager.ChangePathNameNoExtension(item.Url, "-small");

                                MediaType mediatype = await context.MediaType.Where(m => m.Id == item.DocumentType.Id).FirstOrDefaultAsync();
                                if (mediatype != null)
                                {
                                    item.DocumentType = mediatype;
                                }

                                var editFile = mapper.Map<FileData>(item);
                                listFileToAdd.Add(editFile);
                            }
                            else
                            {
                                //existing 
                                listFileToAdd.Add(existFile);
                            }
                        }
                        mediaOnDb.File = listFileToAdd;
                    }
                }

                context.Media.Update(mediaOnDb);
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
            catch (Exception ex)
            {
                return BadRequest("Error: "+ex);

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
