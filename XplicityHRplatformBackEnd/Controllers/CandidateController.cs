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
        public async Task<IEnumerable<CandidateDto>> Get()
        {
            var response = await _dbContext.Candidates.ToListAsync();
            var candidates = new List<CandidateDto>();
            foreach (var candidate in response)
            {
                List<Guid> technologyIds = await _dbContext.CandidateTechnologies
                    .Where(t => t.CandidateId == candidate.Id)
                    .Select(t => t.TechnologyId)
                    .ToListAsync();
                var technologies = new List<Technology>();
                foreach (var technologyId in technologyIds)
                {
                      var technology = await _dbContext.Technologies
                     .Where(t => t.Id == technologyId)
                     .Select(te => te)
                     .FirstOrDefaultAsync();
                    technologies.Add(technology);
                }

                List<Guid> CallDateIds = await _dbContext.CandidateCalldates
                    .Where(t => t.CandidateId == candidate.Id)
                    .Select(t => t.CallDateId)
                    .ToListAsync();
                var callDates = new List<CallDate>();
                foreach (var callDateId in CallDateIds)
                {
                    var callDate = await _dbContext.Calldates
                   .Where(c => c.Id == callDateId)
                   .Select(c => c)
                   .FirstOrDefaultAsync();
                    callDates.Add(callDate);
                }

                var newCandidate = new CandidateDto(callDates, technologies, candidate);

                candidates.Add(newCandidate);
            }
            return candidates;
        }

        [HttpPost]
        public async Task<StatusCodeResult> AddCandidate([FromBody] CandidateDto request)
        {
            var newCandidate = new Candidate(request);

            Guid newCandidateId = await _dataUtilities.AddEntry(_dbContext.Candidates, newCandidate);

            foreach (CallDate date in request.PastCallDates)
            {
                var callDate = await _dbContext.Calldates
                    .Where(c => c.DateOfCall == date.DateOfCall)
                    .SingleOrDefaultAsync();
                if (callDate != null)
                {
                    CandidateCallDate candidateCall1 = new() { CallDateId = callDate.Id, CandidateId = newCandidateId };
                    Guid candidateCalldateId1 = await _dataUtilities.AddEntry(_dbContext.CandidateCalldates, candidateCall1);
                    continue;
                }
               
                Guid newId = await _dataUtilities.AddEntry(_dbContext.Calldates, date);

                CandidateCallDate candidateCall = new() { CallDateId = newId, CandidateId = newCandidateId};
                Guid candidateCalldateId = await _dataUtilities.AddEntry(_dbContext.CandidateCalldates, candidateCall);
            }

            var technologies = new List<Technology>();
            foreach (Technology tech in request.Technologies)
            {
                CandidateTechnology candidateTechnology = new() { TechnologyId = tech.Id, CandidateId = newCandidateId };
                Guid candidateCalldateId = await _dataUtilities.AddEntry(_dbContext.CandidateTechnologies, candidateTechnology);
            }
            return Ok();
        }

        //[httpput]
        //public async task<statuscoderesult> updatecandidate([frombody] candidatedto request)
        //{
        //    candidate updatedcandidate = new candidate(request);

        //    await _dbcontext.candidatetechnologies
        //        .where(ct => ct.candidateid == updatedcandidate.id)
        //        .tolistasync();

        //    await _datautilities.updateentry(_dbcontext.candidatetechnologies, request.technologies);
        //}


        [HttpDelete]
        [Route("{IdToRemove}")]
        public async Task<StatusCodeResult> DeleteCandidate([FromRoute] Guid IdToRemove)
        {
            Candidate candidate = new() { Id = IdToRemove };
            var candidateCallDates = _dbContext.CandidateCalldates
                .Where(cc => cc.CandidateId == IdToRemove).ToList();

            candidateCallDates.ForEach(cc =>
            {
                var success = _dataUtilities.RemoveEntry(_dbContext.CandidateCalldates, cc, true);
            });

            var candidateTechnologies = _dbContext.CandidateTechnologies
                .Where(ct => ct.CandidateId == IdToRemove).ToList();

            candidateTechnologies.ForEach( ct =>
            {
                var success = _dataUtilities.RemoveEntry(_dbContext.CandidateTechnologies, ct, true);
            });

            var success = await _dataUtilities.RemoveEntry(_dbContext.Candidates, candidate, true);
            return Ok();
        }
    }
}
