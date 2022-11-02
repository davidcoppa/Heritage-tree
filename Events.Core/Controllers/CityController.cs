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
using Events.Core.Common.Queryable;
using Events.Core.Common.Helpers;

namespace Events.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CityController : Controller
    {
        private readonly EventsContext context;
        private readonly IMapper mapper;
        private readonly IDataValidator validator;
        private readonly IMessages messages;
        private readonly IHelper helper;
        public CityController(EventsContext context,
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

       

        [HttpGet("GetFilterCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFilterCity([FromQuery] string sort, [FromQuery] string order, [FromQuery] string page, [FromQuery] string itemsPage, [FromQuery] string search)
        {
            try
            {
                IQueryable<City> data = context.City.AsQueryable();
                if (search == null)
                {
                    data = context.City.AsQueryable<City>();
                }
                else
                {
                    data = context.City.Where(x => x.Code.Contains(search)
                                 || x.Name.Contains(search))
                                    .AsQueryable<City>();

                }

                if (!data.Any())
                {
                    return Ok(null);
                }

                var ret = helper.GetFilter<City>(sort, order, page, itemsPage, data);

                return Ok(ret);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.Message);
            }
        }



        // POST: Locations/Create
        [HttpPost("CreateCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateCountry(CityCreateDTO location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(messages.BadRequestModelInvalid);
            }

            var locationMap = mapper.Map<City>(location);


            context.Add(locationMap);
            await context.SaveChangesAsync();

            return Ok(locationMap);
        }



        // POST: Locations/Edit/5
        [HttpPost("EditCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditCity(int id, CityEditDTO location)
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
                City cityDB = await context.City.FindAsync(id);
                if (cityDB == null)
                {
                    return NotFound();
                }
                City city = mapper.Map<City>(location);

                context.Entry(cityDB).CurrentValues.SetValues(city);

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
        [HttpGet("GetCityAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCityAll()
        {
            List<City> countryLocation = await context.City.ToListAsync();

            return Ok(countryLocation);
        }

        // GET: Locations/Details/5
        [HttpGet("GetCity")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await context.City.FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
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

            var location = await context.City.FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            context.City.Remove(location);
            await context.SaveChangesAsync();
            return NoContent();
        }
        private bool LocationExists(int id)
        {
            return context.City.Any(e => e.Id == id);
        }
    }
}
