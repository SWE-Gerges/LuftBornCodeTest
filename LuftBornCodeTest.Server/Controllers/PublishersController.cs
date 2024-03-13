
using LuftBornCodeTest.Server.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LuftBornCodeTest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly IPublishersService _publishersService;


        public PublishersController(IPublishersService publishersService)
        {
           
            _publishersService = publishersService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var publishers = await _publishersService.GetAll();

            return Ok(publishers);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(PublisherDto createPublisherDto)
        {
            var publisher = new Publisher
            {
                Name = createPublisherDto.Name
            };
            await _publishersService.Add(publisher);
            

            return Ok(publisher);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateAsync(byte Id, [FromBody] PublisherDto publisherDto)
        {
            var publisher = await _publishersService.FindById(Id);
            if(publisher == null)
            {
                return NotFound($"No Publisher was found with ID: {Id}");
            }

            publisher.Name = publisherDto.Name;

            _publishersService.Update(publisher);

            return Ok(publisher);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var publisher = await _publishersService.FindById(id);
            if(publisher == null)
            {
                return NotFound($"No Publisher was found with ID: {id}");
            }
            _publishersService.Remove(publisher);
            return Ok();
        }
    }
}
