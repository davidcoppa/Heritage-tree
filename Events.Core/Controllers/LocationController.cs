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

            var ret = GenerateReturnValuesCountry(countryLocation);

            return Ok(ret);
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
                    Latitude = states.Latitude,
                    Longitude = states.Longitude,
                    Name = states.Name,
                    Region = states.Region,
                    FullName = strnName
                };

                retValList.Add(retval);
            }
            return retValList;
        }

        private object GenerateReturnValuesCountry(List<Country> countryLocation)
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



        private List<T> GetFilter<T>(string sort, string order, string page, string itemsPage, IQueryable<T> data) where T : class
        {

            data = OrderByExtension.OrderBy<T>(data, sort, order);

            int itemsPageInt = int.TryParse(itemsPage, out int items) ? items : Int32.MaxValue;
            Pagination pagination = new Pagination(data.Count(), itemsPageInt);

            int pageIndex = int.TryParse(page, out int count) ? count : 0;

            List<T> result = data.PagedIndex(pagination, pageIndex).ToList();

            return result;



        }


        //testing test branch
        [HttpGet("GetFilterCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFilterCountry([FromQuery] string sort, [FromQuery] string order, [FromQuery] string page, [FromQuery] string itemsPage, [FromQuery] string search)
        {
            try
            {
                IQueryable<Country> data = context.Country.AsQueryable();
                if (search == null)
                {
                    data = context.Country.AsQueryable<Country>().Include(x => x.States).ThenInclude(c => c.Cities);
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


                var result = GetFilter<Country>(sort, order, page, itemsPage, data);

                var ret = GenerateReturnValuesCountry(result);

                return Ok(ret);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.Message);

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


                var result = GetFilter<States>(sort, order, page, itemsPage, data);

                var ret = GenerateReturnValuesStates(result);

                return Ok(ret);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.Message);

            }
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

                var result = GetFilter<City>(sort, order, page, itemsPage, data);

                var ret = mapper.Map<CityReturnDTO>(result);

                return Ok(ret);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.Message);
            }
        }



        // POST: Locations/Create
        [HttpPost("CreateCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateCountry(CountryCreateDTO location)
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
        [HttpPost("EditCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EditCountry(int id, CountryEditDTO location)
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
