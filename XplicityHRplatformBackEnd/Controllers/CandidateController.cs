using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
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
        public IEnumerable<CandidateDto> Get()
        {
            var response = _dbContext.Candidates.ToList();
            var candidates = new List<CandidateDto>();
            foreach (var candidate in response)
            {
                List<Guid> technologyIds =_dbContext.CandidateTechnologies
                    .Where(t => t.CandidateId == candidate.Id)
                    .Select(t => t.TechnologyId)
                    .ToList();
                var technologies = new List<string>();
                foreach (var technologyId in technologyIds)
                {
                      var technology = _dbContext.Technologies
                     .Where(t => t.Id == technologyId)
                     .Select(te => te.Title)
                     .FirstOrDefault();
                    technologies.Add(technology);
                }

                List<Guid> CallDateIds = _dbContext.CandidateCalldates
                    .Where(t => t.CandidateId == candidate.Id)
                    .Select(t => t.CallDateId)
                    .ToList();
                var callDates = new List<string>();
                foreach (var callDateId in CallDateIds)
                {
                    var callDate = _dbContext.Calldates
                   .Where(c => c.Id == callDateId)
                   .Select(c => c.DateOfCall)
                   .FirstOrDefault();
                    callDates.Add(callDate);
                }
                var newCandidate = new CandidateDto()
                {
                    FirstName = candidate.FirstName,
                    LastName = candidate.LastName,
                    LinkedIn = candidate.LinkedIn,
                    Comment = candidate.Comment,
                    OpenForSuggestions = candidate.OpenForSuggestions,
                    DateOfFutureCall = candidate.DateOfFutureCall,
                    DatesOfPastCalls = callDates.ToArray(),
                    Technologies = technologies.ToArray()
                };
                candidates.Add(newCandidate);
            }
            return candidates;
        }

        [HttpPost]
        public async Task<StatusCodeResult> AddCandidate([FromBody] CandidateDto request)
        {
            var newCandidate = new Candidate
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                LinkedIn = request.LinkedIn,
                Comment = request.Comment,
                OpenForSuggestions = request.OpenForSuggestions,
                DateOfFutureCall = request.DateOfFutureCall,
            };
            Guid newCandidateId = await _dataUtilities.AddEntry(_dbContext.Candidates, newCandidate);

            foreach (string date in request.DatesOfPastCalls)
            {
                var callDate = await _dbContext.Calldates
                    .Where(c => c.DateOfCall == date)
                    .SingleOrDefaultAsync();
                if (callDate != null)
                {
                    CandidateCallDate candidateCall1 = new CandidateCallDate() { CallDateId = callDate.Id, CandidateId = newCandidateId };
                    Guid candidateCalldateId1 = await _dataUtilities.AddEntry(_dbContext.CandidateCalldates, candidateCall1);
                    continue;
                }
                var newCallDate = new CallDate() { DateOfCall = date };
                Guid newId = await _dataUtilities.AddEntry(_dbContext.Calldates, newCallDate);

                CandidateCallDate candidateCall = new CandidateCallDate() { CallDateId = newId, CandidateId = newCandidateId};
                Guid candidateCalldateId = await _dataUtilities.AddEntry(_dbContext.CandidateCalldates, candidateCall);
            }

            var technologies = new List<Technology>();
            foreach (string tech in request.Technologies)
            {
                Technology technology = _dbContext.Technologies
                    .Where(t => t.Title == tech)
                    .SingleOrDefault();
                CandidateTechnology candidateTechnology = new CandidateTechnology() { TechnologyId = technology.Id, CandidateId = newCandidateId };
                Guid candidateCalldateId = await _dataUtilities.AddEntry(_dbContext.CandidateTechnologies, candidateTechnology);
            }
            return Ok();
        }
    }
}
