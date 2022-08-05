using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using XplicityHRplatformBackEnd.DB;
using XplicityHRplatformBackEnd.Models;

namespace XplicityHRplatformBackEnd.Controllers
{
    [Route("api/candidates")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly DatabaseUtilities _dataUtilities;
        private readonly HRplatformDbContext _dbContext;

        public CandidateController(DatabaseUtilities context, HRplatformDbContext dbContext)
        {
            _dataUtilities = context;
            _dbContext = dbContext;
        }

        [HttpGet]
        public IEnumerable<Candidate> Get()
        {
            var TechnologyIds = _dbContext.CandidateTechnologies.Select(c => c.CandidateId).ToList();
            var response = _dbContext.Candidates.Where(c => TechnologyIds.Contains(c.Id)).ToList();
            return response;
        }
    
        [HttpPost]
        public async Task<ActionResult> AddCandidate([FromBody] CandidateDto candidate)
        {
            var character = await _context.Characters
                .Where(c => c.Id == request.CharacterId)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync();

            var newCandidate = new Candidate
            {
                FirstName = candidate.FirstName,
                LastName = candidate.LastName,
                LinkedIn = candidate.LinkedIn,
                Comment = candidate.Comment,
                OpenForSuggestions = candidate.OpenForSuggestions,
                DateOfFutureCall = candidate.DateOfFutureCall,

            };

            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();

            return await Get(newCharacter.UserId);

        }
        [HttpPost("addCity")]
        public async Task<ActionResult<Character>> AddCharacterSkill(AddCharacterSkillDto request)
        {
            var character = await _context.Characters
                .Where(c => c.Id == request.CharacterId)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync();
            if (character == null)
                return NotFound();

            var skill = await _context.Skills.FindAsync(request.SkillId);
            if (skill == null)
                return NotFound();

            character.Skills.Add(skill);
            await _context.SaveChangesAsync();

            return character;
        }





    }
}
