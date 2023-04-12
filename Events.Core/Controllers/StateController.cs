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
using Events.Core.DTOs;
using AutoMapper;
using Events.Core.Common.Validators;
using Events.Core.Common.Messages;
using Events.Core.Common.Helpers;

namespace Events.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StateController : Controller
    {
        private readonly EventsContext context;
        private readonly IMapper mapper;
        private readonly IDataValidator validator;
        private readonly IMessages messages;
        private readonly IHelper helper;

        public StateController(EventsContext context,
            IMapper mapper,
            IDataValidator validator,
            IMessages messages,
            IHelper helper
            )
        {
            this.context = context;
            this.mapper = mapper;
            this.validator = validator;
            this.messages = messages;
            this.helper = helper;
        }


        private object GenerateReturnValuesStates(List<States> stateLocation)
        {
            List<StateReturnDTO> retValList = new List<StateReturnDTO>();


            foreach (var states in stateLocation ?? Enumerable.Empty<States>())
            {
                string strnName = states.Name;

                foreach (var cities in states.Cities ?? Enumerable.Empty<City>())
                {
                    strnName = strnName + ", " + cities.Name;
                }

                StateReturnDTO retval = new StateReturnDTO
                {
                    Capital = states.Capital,
                    Code = states.Code,
                    Id = states.Id,
                    Coordinates = states.Coordinates,
                    Name = states.Name,
                    Region = states.Region,
                    FullName = strnName
                };

                retValList.Add(retval);
            }
            return retValList;
        }
        //joining to master



        [HttpPost("CreateState")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateState(StateCreateDTO location)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(messages.BadRequestModelInvalid);
                }

                var locationMap = mapper.Map<States>(location);


                context.Add(locationMap);
                await context.SaveChangesAsync();

                return Ok(locationMap);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

                throw;
            }
           
        }




        [HttpGet("GetFilterState")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFilterState([FromQuery] string sort, [FromQuery] string order, [FromQuery] string page, [FromQuery] string itemsPage, [FromQuery] string search)
        {
            try
            {
                IQueryable<States> data = context.State.AsQueryable();
                if (search == null)
                {
                    data = context.State.AsQueryable<States>().Include(x => x.Cities);
                }
                else
                {
                    data = context.State.Where(x => x.Code.Contains(search)
                                 || x.Name.Contains(search))
                                    .Include(x => x.Cities)
                                    .AsQueryable<States>();

                }

                if (!data.Any())
                {
                    return Ok(null);
                }


                var result = helper.GetFilter<States>(sort, order, page, itemsPage, data);

                var ret = GenerateReturnValuesStates(result);

                return Ok(ret);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.Message);

            }
        }



        // POST: Locations/Edit/5
        [HttpPost("EditState")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditState(int id, StateEditDTO location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(messages.BadRequestModelInvalid);
            }

            try
            {
                States countryBD = await context.State.FindAsync(id);
                if (countryBD == null)
                {
                    return NotFound();
                }
                States country = mapper.Map<States>(location);

                context.Entry(countryBD).CurrentValues.SetValues(country);

                if (country.Cities != null && country.Cities.Count > 0)
                {
                    countryBD.Cities = country.Cities;
                    context.Entry(countryBD.Cities).CurrentValues.SetValues(country.Cities);

                }


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



        // GET: Locations
        [HttpGet("GetStateAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStateAll()
        {
            List<States> countryLocation = await context.State.Include(x => x.Cities).ToListAsync();

            var ret = GenerateReturnValuesStates(countryLocation);

            return Ok(ret);
        }

        // GET: Locations/Details/5
        [HttpGet("GetState")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetState(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await context.State.FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }

        // GET: Locations/Delete/5
        [HttpPost("DeleteState/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteState(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await context.State.FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            context.State.Remove(location);
            await context.SaveChangesAsync();
            return NoContent();
        }
        private bool LocationExists(int id)
        {
            return context.State.Any(e => e.Id == id);
        }
    }
}
