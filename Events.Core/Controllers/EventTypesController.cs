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
using Events.Core.Common.Validators;
using Events.Core.Common.Messages;

namespace Events.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventTypesController : Controller
    {
        private readonly EventsContext context;
        private readonly IMapper mapper;
        private readonly IDataValidator validator;
        private readonly IMessages messages;


        public EventTypesController(EventsContext context,
            IMapper mapper,
            IDataValidator validator,
            IMessages messages)
        {
            this.context = context;
            this.mapper = mapper;
            this.validator = validator;
            this.messages = messages;
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
        public async Task<IActionResult> Create(EventTypeCreateDTO eventTypeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(messages.BadRequestModelInvalid);
            }

            EventTypes eventType = mapper.Map<EventTypes>(eventTypeDTO);


            if (validator.ValidateObject<EventTypes>(eventType))
            {
                return BadRequest(messages.BadRequestModelNullOrInvalid);
            }

            //TODO: validate if we already have that event

            List<EventTypes> events = await EventTypeExistByNameOrDescription(eventType);
            if (events.Count > 0)
                return BadRequest(string.Format(messages.EventTypeExistingDatabase, eventType.Name));

            context.Add(eventType);

            await context.SaveChangesAsync();

            return Ok(eventType);
        }

        [HttpPost("EventTypeExistByNameOrDescription")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<EventTypes>> EventTypeExistByNameOrDescription(EventTypes evtt)
        {
            return await context.EventType.Where(x => x.Name == evtt.Name || x.Description == evtt.Description).ToListAsync();
        }


        [HttpPost("Edit/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Edit(int id, EventTypeEditDTO eventTypes)
        {
            if (id != eventTypes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

               // EventTypes eventTypesDb = mapper.Map<EventTypes>(eventTypes);
                EventTypes entity = await context.EventType.FindAsync(id);

                if (entity == null)
                {
                    return NotFound(messages.EventTypeNotFound);

                }
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
            return BadRequest(messages.BadRequestModelInvalid);
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
