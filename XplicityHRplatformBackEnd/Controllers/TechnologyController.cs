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
            //var techs = new List<string>();
            //var technologies = await _context.Technologies.ToListAsync();
            //
            //foreach (var technology in technologies)
            //{
            //    techs.Add(technology.Title);   
            //}
            //return techs;
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

        //[HttpPost] 
        //public async Task<List<Technology>> CreateTechnology(CreateTechnologyDto request)
        //{
        //    var tech = await _context.Technologies.FindAsync(request.Title);
        //    if (tech != null)
        //        return await GetTechnologyAsync(tech.Id);
        //
        //    var newTechnology = new Technology
        //    {
        //        Title = request.Title,
        //    };
        //
        //    _context.Technologies.Add(newTechnology);
        //    await _context.SaveChangesAsync();
        //
        //    var newTechnologyId = (await _context.Technologies.FindAsync(newTechnology.Title)).Id;
        //
        //    return await GetTechnologyAsync(newTechnologyId);
        //}


    }
}
