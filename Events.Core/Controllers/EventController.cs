using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EventsManager.Data;
using EventsManager.Model;
using Events.Core.DTOs;
using AutoMapper;
using Events.Core.Common.Validators;

namespace Events.Core.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EventController : Controller
    {
        private readonly EventsContext context;
        private readonly IMapper mapper;
        private readonly IDataValidator validator;



        public EventController(EventsContext context,
            IMapper mapper,
            IDataValidator validator
            )
        {
            this.context = context;
            this.mapper = mapper;
            this.validator = validator;

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

            var @event = await context.Event.FirstOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }


        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create(EventCreateDTO evt)
        {

            if (ModelState.IsValid)
            {
                Event evento = mapper.Map<Event>(evt);

                if (validator.ValidateObject<Event>(evento))
                {
                    return BadRequest("Model is null or not valid");
                }

                if (evt.Person1 == null)
                {
                    return BadRequest("An event needs at least one person");
                }
                else
                {
                    Person son = await context.Person.Where(x => x.Id == evt.Person1.Id).FirstOrDefaultAsync();
                    if (son == null)
                    {
                        if (validator.ValidateObject<Person>(evt.Person1))
                        {
                            return NotFound("Person1");
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
                            return NotFound("Person2");
                        }
                        context.Add(evt.Person2);
                        mom = evt.Person2;
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
                            return NotFound("Person3");
                        }
                        context.Add(evt.Person3);
                        dad = evt.Person3;
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
                            return NotFound("Event");
                        }
                        context.Add(evt.EventType);
                        eventstypes = evt.EventType;
                    }

                    evt.EventType = eventstypes;
                }
                if (evt.photos != null)
                {
                    List<Photos> photos = new List<Photos>();
                    foreach (var photo in evt.photos)
                    {
                        var photoElement = await context.Photos.Where(x => x.Id == photo.Id).FirstOrDefaultAsync();
                        if (photoElement == null)
                        {
                            if (validator.ValidateObject<Photos>(photo))
                            {
                                return NotFound("photo");
                            }
                            context.Add(photo);
                            photoElement = photo;
                        }
                        photos.Add(photoElement);
                    }

                    evt.photos = photos;
                }

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
        public async Task<IActionResult> Edit(int id, EventCreateEditDTO eventEdit)
        {
            if (id != eventEdit.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Event evento = mapper.Map<Event>(eventEdit);
                Person entity = await context.Person.FindAsync(id);

                if (entity != null)
                {
                    try
                    {
                        context.Entry(entity).CurrentValues.SetValues(evento);

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
                return NotFound("We can't edit the event");

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

            var @event = await context.Event.FirstOrDefaultAsync(m => m.Id == id);
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
            return context.Event.Any(e => e.Id == id);
        }
    }
}
