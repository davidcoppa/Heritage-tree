using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventsManager.Data;
using EventsManager.Model;
using Events.Core.DTOs;
using AutoMapper;
using Events.Core.Common.Validators;
using Events.Core.Common.Queryable;
using Events.Core.Common.Messages;

namespace Events.Core.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        private readonly EventsContext context;
        private readonly IMapper mapper;
        private readonly IDataValidator validator;
        private readonly IMessages messages;

        // private readonly EventTypesController ctrlEventType;


        public EventController(EventsContext context,
            IMapper mapper,
            IDataValidator validator,
            IMessages messages
            )
        {
            this.context = context;
            this.mapper = mapper;
            this.validator = validator;
            this.messages = messages;
        }

        // GET: Events
        [HttpGet("Get")]
        public async Task<IActionResult> Index()
        {
            var events = await context.Event.ToListAsync();
            return Ok(events);
        }

        // GET: Event
        [HttpGet("GetFilter")]
        public async Task<IActionResult> GetFilter([FromQuery] string sort, [FromQuery] string order, [FromQuery] string page, [FromQuery] string itemsPage, [FromQuery] string? search)
        {
            try
            {

                IQueryable<Event> data = context.Event.AsQueryable();
                if (search == null)
                {
                    data = context.Event.AsQueryable<Event>().Include(x => x.Person1)
                                                              .Include(x => x.Person2)
                                                              .Include(x => x.Person3)
                                                              .Include(x => x.EventType)
                                                              .Include(x => x.Media);
                }
                else
                {
                    data = context.Event.Where(x => x.Title.Contains(search)
                                 || x.Description.Contains(search))
                                        .Include(x => x.Person1)
                                        .Include(x => x.Person2)
                                        .Include(x => x.Person3)
                                        .Include(x => x.EventType)
                                        .Include(x => x.Media)
                                        .AsQueryable<Event>();

                }

                if (!data.Any())
                {
                    return Ok(null);
                }
                data = OrderByExtension.OrderBy(data, sort, order);

                int itemsPageInt = int.TryParse(itemsPage, out int items) ? Int32.MaxValue : items;
                Pagination pagination = new Pagination(data.Count(), itemsPageInt);

                int pageIndex = int.TryParse(page, out int count) ? 0 : count;

                List<Event>? result = data.PagedIndex(pagination, pageIndex).ToList();

                return Ok(result);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.Message);

            }
        }



        // GET: Events/Details/5
        [HttpGet("Details")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await context.Event.FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }


        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(EventCreateDTO evento)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(messages.BadRequestModelInvalid);
                }

                Event evt = mapper.Map<Event>(evento);

                if (validator.ValidateObject<Event>(evt))
                {
                    return BadRequest(messages.BadRequestModelNullOrInvalid);
                }

                if (evt.Person1 == null)
                {
                    return BadRequest(messages.EventEmpty);
                }
                else
                {
                    Person son = await context.Person.Where(x => x.Id == evt.Person1.Id).FirstOrDefaultAsync();
                    if (son == null)
                    {
                        if (validator.ValidateObject<Person>(evt.Person1))
                        {
                            return NotFound("Son");
                        }
                        context.Add(evt.Person1);

                        son = evt.Person1;

                    }
                    evt.Person1 = son;

                }
                if (evt.Person2 != null)
                {
                    Person mom = await context.Person.Where(x => x.Id == evt.Person2.Id).FirstOrDefaultAsync();
                    if (mom == null)
                    {
                        if (validator.ValidateObject<Person>(evt.Person2))
                        {
                            return NotFound("Father");
                        }
                        mom = evt.Person2;
                        context.Add(evt.Person2);

                    }
                    evt.Person2 = mom;


                }
                if (evt.Person3 != null)
                {
                    Person dad = await context.Person.Where(x => x.Id == evt.Person3.Id).FirstOrDefaultAsync();
                    if (dad == null)
                    {
                        if (validator.ValidateObject<Person>(evt.Person3))
                        {
                            return NotFound("Mother");
                        }
                        dad = evt.Person3;
                        context.Add(evt.Person3);

                    }
                    evt.Person3 = dad;


                }
                if (evt.EventType != null)
                {
                    EventTypes eventstypes = await context.EventType.Where(x => x.Id == evt.EventType.Id).FirstOrDefaultAsync();
                    if (eventstypes == null)
                    {
                        if (validator.ValidateObject<EventTypes>(evt.EventType))
                        {
                            return NotFound("Event type");
                        }
                        eventstypes = evt.EventType;
                        context.Add(evt.EventType);

                    }
                    evt.EventType = eventstypes;

                }
                if (evt.Media != null)
                {
                    List<Media> mediaList = new List<Media>();
                    foreach (var media in evt.Media)
                    {

                        var photoElement = await context.Photos.Where(x => x.Id == media.Id).FirstOrDefaultAsync();
                        if (photoElement == null)
                        {
                            if (validator.ValidateObject<Media>(media))
                            {
                                return NotFound("photo");
                            }
                            context.Add(media);
                            photoElement = media;
                        }
                        mediaList.Add(photoElement);
                    }

                    evt.Media = mediaList;
                }

                context.Event.Add(evt);
                await context.SaveChangesAsync();
                return Ok(evt);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        // POST: Events/Edit/5
        [HttpPost("Edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Edit(int id, EventCreateEditDTO eventEdit)
        {
            //  var pp = eventEdit as ;
            if (id != eventEdit.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Event evento = mapper.Map<Event>(eventEdit);
                Event entity = await context.Event.FindAsync(id);

                if (entity == null)
                {
                    return NotFound(messages.EventNotFound);
                }
                try
                {
                    context.Entry(entity).CurrentValues.SetValues(evento);

                    if (evento.Person1 != null)
                    {
                        Person pp = await context.Person.Where(x => x.Id == evento.Person1.Id).FirstOrDefaultAsync();
                        entity.Person1 = pp;
                        if (entity.Person1 == null)
                        {
                            entity.Person1 = pp;
                            context.Event.Update(entity);
                        }
                        else
                        {
                            context.Entry(entity.Person1).CurrentValues.SetValues(evento.Person1);

                        }

                    }

                    if (evento.Person2 != null)
                    {
                        Person pp = await context.Person.Where(x => x.Id == evento.Person2.Id).FirstOrDefaultAsync();
                        entity.Person2 = pp;
                        if (entity.Person2 == null)
                        {
                            entity.Person2 = pp;
                            context.Event.Update(entity);
                        }
                        else
                        {
                            context.Entry(entity.Person2).CurrentValues.SetValues(evento.Person2);

                        }

                    }
                    if (evento.Person3 != null)
                    {
                        Person pp = await context.Person.Where(x => x.Id == evento.Person3.Id).FirstOrDefaultAsync();
                        entity.Person3 = pp;
                        if (entity.Person3 == null)
                        {
                            entity.Person3 = pp;
                            context.Event.Update(entity);
                        }
                        else
                        {
                            context.Entry(entity.Person3).CurrentValues.SetValues(evento.Person3);

                        }

                    }


                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(evento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(evento);
            }
            return BadRequest(messages.BadRequestModelInvalid);
        }

     
     
      
        // GET: Events/Delete/5
        [HttpPost("Delete/{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await context.Event.FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }
            context.Event.Remove(@event);
            await context.SaveChangesAsync();

            return NoContent();
        }

        public EventTypes GetDataEventType()
        {
            EventTypes? entityList = context.EventType.Where(x => x.Name == "Nacimiento" || x.Description == "Nacimiento").FirstOrDefault();

            EventTypes entity = entityList ?? new EventTypes
            {
                Description = "Nacimiento",
                Name = "Nacimiento"
            };

            return entity;

        }

        private bool EventExists(int id)
        {
            return context.Event.Any(e => e.Id == id);
        }

    }
}
