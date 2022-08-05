using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XplicityHRplatformBackEnd.DB;
using XplicityHRplatformBackEnd.Models;

namespace XplicityHRplatformBackEnd.Controllers
{
    [Route("api/technologies")]
    [ApiController]
    public class TechnologyController : ControllerBase
    {
        private readonly HRplatformDbContext _context;
        public TechnologyController(HRplatformDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Technology>>> GetAllTechnologies()
        {
            return await _context.Technologies.ToListAsync();
        }

        [Route("{technologyId}")]
        [HttpGet]
        public async Task<List<Technology>> GetTechnologyAsync(int technologyId)
        {
            var technologies = await _context.Technologies
                .Where(t => t.Id == technologyId)
                .ToListAsync();

            return technologies;
        }

        [HttpPost]

        public async Task<List<Technology>> CreateTechnology([FromBody] CreateTechnologyDto request)
        {
            var tech = await _context.Technologies.Where(t => t.Title == request.Title).FirstOrDefaultAsync();
            if (tech != null)
                return await GetTechnologyAsync(tech.Id);

            var newTechnology = new Technology
            {
                Title = request.Title,
            };

            _context.Technologies.Add(newTechnology);
            await _context.SaveChangesAsync();
        
            var newTechnologyId = (await _context.Technologies.Where(t => t.Title == request.Title).FirstOrDefaultAsync()).Id;
        
            return await GetTechnologyAsync(newTechnologyId);
        }


    }
}
