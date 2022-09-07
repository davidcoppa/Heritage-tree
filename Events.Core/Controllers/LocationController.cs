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

namespace Events.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LocationController : Controller
    {
        private readonly EventsContext context;
        private readonly IMapper mapper;
        private readonly IDataValidator validator;
        private readonly IMessages messages;

        public LocationController(EventsContext context,
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



        // GET: Locations
        [HttpGet("Get")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Index()
        {
            List<Country> countryLocation = await context.Country.Include(x => x.States).ThenInclude(c => c.Cities).ToListAsync();

            var ret = GenerateReturnValues(countryLocation);

            return Ok(ret);
        }

        private object GenerateReturnValues(List<Country> countryLocation)
        {
            List<CountryReturnDTO> retValList = new List<CountryReturnDTO>();

            foreach (var country in countryLocation ?? Enumerable.Empty<Country>())
            {
                string strnName = country.Name;
                foreach (var states in country.States ?? Enumerable.Empty<States>())
                {
                    strnName = strnName + ", " + states.Name;
                    foreach (var cities in states.Cities ?? Enumerable.Empty<City>())
                    {
                        strnName = strnName + ", " + cities.Name;
                    }
                }

                // CountryReturnDTO retval = mapper.Map<CountryReturnDTO>(countryLocation);
                CountryReturnDTO retval = new CountryReturnDTO
                {
                    Capital = country.Capital,
                    Code = country.Code,
                    Id = country.Id,
                    Latitude = country.Latitude,
                    Longitude = country.Longitude,
                    Name = country.Name,
                    Region = country.Region,
                    State = country.States,
                    FullName = strnName
                };

                retValList.Add(retval);
            }

            return retValList;
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

            var location = await context.Country.FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return Ok(location);
        }


        [HttpGet("GetFilter")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFilter([FromQuery] string sort, [FromQuery] string order, [FromQuery] string page, [FromQuery] string itemsPage, [FromQuery] string search)
        {
            try
            {

                IQueryable<Country> data = context.Country.AsQueryable();
                if (search == null)
                {
                    data = context.Country.AsQueryable<Country>().Include(x => x.States).ThenInclude(c => c.Cities) ;
                }
                else
                {
                    data = context.Country.Where(x => x.Code.Contains(search)
                                 || x.Name.Contains(search))
                                    .Include(x => x.States)
                                    .ThenInclude(c => c.Cities)
                                    .AsQueryable<Country>();

                }

                if (!data.Any())
                {
                    return Ok(null);
                }
                data = OrderByExtension.OrderBy(data, sort, order);

                int itemsPageInt = int.TryParse(itemsPage, out int items) ? items : Int32.MaxValue;
                Pagination pagination = new Pagination(data.Count(), itemsPageInt);

                int pageIndex = int.TryParse(page, out int count) ? count : 0;

                List<Country> result = data.PagedIndex(pagination, pageIndex).ToList();


                var ret = GenerateReturnValues(result);

                return Ok(ret);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.Message);

            }
        }



        // POST: Locations/Create
        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create(CountryCreateDTO location)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(messages.BadRequestModelInvalid);
            }

            var locationMap = mapper.Map<Country>(location);


            context.Add(locationMap);
            await context.SaveChangesAsync();

            return Ok(locationMap);
        }



        // POST: Locations/Edit/5
        [HttpPost("Edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Edit(int id, CountryEditDTO location)
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
                Country countryBD = await context.Country.FindAsync(id);
                if (countryBD == null)
                {
                    return NotFound();
                }
                Country country = mapper.Map<Country>(location);

                context.Entry(countryBD).CurrentValues.SetValues(country);

                if (country.States != null)
                {
                    countryBD.States = country.States;
                    context.Entry(countryBD.States).CurrentValues.SetValues(country.States);

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

            var location = await context.Country.FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            context.Country.Remove(location);
            await context.SaveChangesAsync();
            return NoContent();
        }
        private bool LocationExists(int id)
        {
            return context.Country.Any(e => e.Id == id);
        }
    }
}
