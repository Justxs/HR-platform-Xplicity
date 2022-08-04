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
        public async Task<List<string>> GetTechnologiesAsync()
        {
            var techs = new List<string>();
            var technologies = await _context.Technologies.ToListAsync();

            foreach (var technology in technologies)
            {
                techs.Add(technology.Title);   
            }
            return techs;
        }
    }
}
