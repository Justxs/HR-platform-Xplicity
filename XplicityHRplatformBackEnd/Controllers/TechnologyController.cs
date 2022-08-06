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
        public async Task<List<Technology>> GetAllTechnologies()
        {
            return await _dataUtilities.GetAll(_dbContext.Technologies);
        }

        [HttpPost]

        public async Task<StatusCodeResult> CreateTechnology([FromBody] Technology request)
        {
            if (request == null) return Ok();
            var tech = await _dbContext.Technologies.Where(t => t.Title == request.Title).FirstOrDefaultAsync();
            if (tech != null)
            {
                return Ok();
            }

            if (await _dataUtilities.AddEntry(_dbContext.Technologies, request) != null)
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
