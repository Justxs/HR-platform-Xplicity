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
        private readonly DatabaseUtilities _dataUtilities;
        private readonly HRplatformDbContext _dbContext;

        public TechnologyController(DatabaseUtilities context, HRplatformDbContext dbContext)
        {
            _dataUtilities = context;
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Technology>>> GetAllTechnologies()
        {
            return await _dataUtilities.GetAll();
        }

        [Route("{technologyId}")]
        [HttpGet]
        public async Task<Technology> GetTechnologyAsync(Guid technologyId)
        {
            return await _dataUtilities.GetById(_dbContext.Technologies, technologyId);
        }

        [HttpPost]

        public async Task<ActionResult<Guid>> CreateTechnology([FromBody] CreateTechnologyDto request)
        {

            var tech = await _dbContext.Technologies.Where(t => t.Title == request.Title).FirstOrDefaultAsync();
            if (tech != null)
            {
                return tech.Id;
            }

            var newTechnology = new Technology
            {
                Title = request.Title,
            };

            return await _dataUtilities.AddEntry(_dbContext.Technologies, newTechnology);
        }
    }
}
