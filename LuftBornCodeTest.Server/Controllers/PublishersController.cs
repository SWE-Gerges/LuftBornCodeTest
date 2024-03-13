
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LuftBornCodeTest.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublishersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;


        public PublishersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var publishers = await _context.Publishers.OrderBy(p => p.Name).ToListAsync();

            return Ok(publishers);
        }

        [HttpPost]
        public async Task<IActionResult> AddAsync(PublisherDto createPublisherDto)
        {
            var publisher = new Publisher
            {
                Name = createPublisherDto.Name
            };
            await _context.Publishers.AddAsync(publisher);
            _context.SaveChanges();

            return Ok(publisher);
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> UpdateAsync(int Id, [FromBody] PublisherDto publisherDto)
        { 
            var publisher = await _context.Publishers.SingleOrDefaultAsync(p => p.Id == Id);
            if(publisher == null)
            {
                return NotFound($"No Publisher was found with ID: {Id}");
            }

            publisher.Name = publisherDto.Name;

            _context.SaveChanges();

            return Ok(publisher);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteAsync(int id)
        {
            var publisher = await _context.Publishers.SingleOrDefaultAsync(p => p.Id ==id);
            if(publisher == null)
            {
                return NotFound($"No Publisher was found with ID: {id}");
            }
            _context.Publishers.Remove(publisher);
            _context.SaveChanges();
            return Ok();
        }
    }
}
