using AutoMapper;
using Events.Core.Common.Messages;
using Events.Core.Common.Queryable;
using Events.Core.DTOs;
using Events.Core.Model;
using EventsManager.Data;
using Microsoft.AspNetCore.Mvc;

namespace Events.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagController : Controller
    {
        private readonly EventsContext context;
        private readonly IMapper mapper;
        private readonly IMessages messages;
        public TagController(EventsContext context, IMapper mapper, IMessages messages)
        {
            this.context = context;
            this.mapper = mapper;
            this.messages = messages;
        }


        [HttpGet("GetFilter")]
        public async Task<IActionResult> GetFilter([FromQuery] string sort, [FromQuery] string order, [FromQuery] string page, [FromQuery] string itemsPage, [FromQuery] string? search)
        {
            try
            {

                IQueryable<Tags> data = context.Tags.AsQueryable();
                if (search == null)
                {
                    data = context.Tags.AsQueryable<Tags>();
                }
                else
                {
                    data = context.Tags.Where(x => x.Name.Contains(search)).AsQueryable<Tags>();

                }

                if (!data.Any())
                {
                    return Ok(null);
                }
                data = OrderByExtension.OrderBy(data, sort, order);

                int itemsPageInt = int.TryParse(itemsPage, out int items) ? Int32.MaxValue : items;
                Pagination pagination = new Pagination(data.Count(), itemsPageInt);

                int pageIndex = int.TryParse(page, out int count) ? 0 : count;

                List<Tags>? result = data.PagedIndex(pagination, pageIndex).ToList();

                return Ok(result);


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(ex.Message);

            }
        }


        [HttpPost("Create")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create(TagsDTO tagDto)
        {
            if (!ModelState.IsValid)
            {
                return Ok();
            }

            var addTag = mapper.Map<Tags>(tagDto);
        

            context.Add(addTag);
            await context.SaveChangesAsync();

        
            return Ok(addTag);
        }


        [HttpPost("Edit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Edit(int id, TagsDTO tagDto)
        {
            if (id != tagDto.Id)
            {
                return NotFound();
            }

            try
            {
                var editTag = mapper.Map<Tags>(tagDto);

                Tags entity = await context.Tags.FindAsync(id);
                if (entity == null)
                {
                    return NotFound(messages.PersonNotFound);
                }


                context.Entry(entity).CurrentValues.SetValues(editTag);
                await context.SaveChangesAsync();


                return Ok(editTag);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest();
            }
        }

    }
}
