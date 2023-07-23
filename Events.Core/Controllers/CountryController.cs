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
    public class CountryController : Controller
    {
        private readonly EventsContext context;
        private readonly IMapper mapper;
        private readonly IDataValidator validator;
        private readonly IMessages messages;
        private readonly IHelper helper;

        public CountryController(EventsContext context,
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



        private object GenerateReturnValuesCountry(List<Country> countryLocation)
        {
            List<CountryReturnDTO> retValList = new List<CountryReturnDTO>();

            foreach (var country in countryLocation ?? Enumerable.Empty<Country>())
            {
               // string strnName = country.Name;
                //foreach (var states in country.States ?? Enumerable.Empty<States>())
                //{
                //    strnName = strnName + ", " + states.Name;
                //    foreach (var cities in states.Cities ?? Enumerable.Empty<City>())
                //    {
                //        strnName = strnName + ", " + cities.Name;
                //    }
                //}

                CountryReturnDTO retval = new CountryReturnDTO
                {
                    Capital = country.Capital,
                    Code = country.Code,
                    Id = country.Id,
                    Lat = country.Lat,
                    Lng = country.Lng,
                    Name = country.Name,
                    Region = country.Region,
                    State = country.States,
                    FullName = country.Name
                };

                retValList.Add(retval);
            }

            return retValList;
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


                List<Country> result = helper.GetFilter<Country>(sort, order, page, itemsPage, data);

                var ret = GenerateReturnValuesCountry(result);

                return Ok(ret);
           //     return Ok();


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

            if (validator.ValidateObject<Country>(locationMap))
            {
                return BadRequest(messages.BadRequestModelNullOrInvalid);
            }

            if (locationMap.Name==null)
            {
                return BadRequest(messages.CountryEmpty);
            }

            if (locationMap.States!=null&& locationMap.States.Count>0)
            {
                foreach (var state in locationMap.States)
                {
                    var stateBD = await context.State.Where(x => x.Id == state.Id).FirstOrDefaultAsync();
                    
                    if (stateBD != null)
                    {
                        context.Add(stateBD);
                    }
                 //   locationMap.States.Add(stateBD);
                }
               
            }

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

                if (location.States != null)
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



        // GET: Locations
        [HttpGet("GetCountryAll")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountryAll()
        {
            //List<Country> countryLocation = await context.Country.Include(x => x.States).ThenInclude(c => c.Cities).ToListAsync();

            //var ret = GenerateReturnValuesCountry(countryLocation);

            //return Ok(ret);
            return Ok();
        }

        // GET: Locations/Details/5
        [HttpGet("GetCountry")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCountry(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var location = null;// await context.Country.FirstOrDefaultAsync(m => m.Id == id);
            //if (location == null)
            //{
            //    return NotFound();
            //}

            //return Ok(location);
            return Ok();
        }


        // GET: Locations/Delete/5
        [HttpPost("Delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            //var location = await context.Country.FirstOrDefaultAsync(m => m.Id == id);
            //if (location == null)
            //{
            //    return NotFound();
            //}

            //context.Country.Remove(location);
            //await context.SaveChangesAsync();
            return NoContent();
        }
        private bool LocationExists(int id)
        {
            return true;
           // return context.Country.Any(e => e.Id == id);
        }
    }
}
