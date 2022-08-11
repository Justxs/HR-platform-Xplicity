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
        public async Task<StatusCodeResult> AddCandidates([FromBody] List<CandidateDto> requests)
        {
            foreach (CandidateDto request in requests)
            {
                bool exists = false;
                var candidates = await _dataUtilities.GetAll(_dbContext.Candidates);
                foreach (Candidate candidate in candidates)
                {
                    if ( candidate.FirstName == request.FirstName 
                        && candidate.LastName == request.LastName
                        && candidate.LinkedIn == request.LinkedIn)
                    {
                        exists = true; 
                        break;
                    }
                }
                if (exists) break;

                var newCandidate = new Candidate(request);
                Guid newCandidateId = await _dataUtilities.AddEntry(_dbContext.Candidates, newCandidate);

                await AddCallDateRelations(request.PastCallDates, newCandidateId);
                await AddTechnologyRelations(request.Technologies, newCandidateId);
                await _dbContext.SaveChangesAsync();
            }
            return Ok();
        }

        [HttpPut]
        public async Task<StatusCodeResult> UpdateCandidate([FromBody] CandidateDto request)
        {
            Candidate updatedCandidate = new (request);

            var CallDatesRemoved = await RemoveCallDateRelations(request.Id);
            var TechnologiesRemoved = await RemoveTechnologyRelations(request.Id);

            if (CallDatesRemoved && TechnologiesRemoved)
            {
                _dbContext.Candidates.Update(updatedCandidate);
            }

            await AddCallDateRelations(request.PastCallDates, request.Id);
            await AddTechnologyRelations(request.Technologies, request.Id);
            await SaveToDbAsync();
            return Ok();
        }

        [HttpDelete]
        [Route("{IdToRemove}")]
        public async Task<StatusCodeResult> DeleteCandidate([FromRoute] Guid IdToRemove)
        {
            Candidate candidate = new() { Id = IdToRemove };

            var callDatesRemoved = await RemoveCallDateRelations(IdToRemove);
            var TechnologiesRemoved = await RemoveTechnologyRelations(IdToRemove);

            if (callDatesRemoved && TechnologiesRemoved)
            {
                _dbContext.Candidates.Remove(candidate);
                await SaveToDbAsync();
                return Ok();
            }
            return BadRequest();
        }

        private async Task<bool> AddCallDateRelations (List<CallDate> PastCallDates, Guid CandidateId )
        {
            try
            { 
                foreach (CallDate date in PastCallDates)
                {
                    var callDate = await _dbContext.Calldates
                        .Where(c => c.DateOfCall == date.DateOfCall)
                        .SingleOrDefaultAsync();
                    if (callDate != null)
                    {
                        CandidateCallDate candidateCallDate = new() { CallDateId = callDate.Id, CandidateId = CandidateId };
                        await _dbContext.CandidateCalldates.AddAsync(candidateCallDate);
                        continue;
                    }

                    await _dbContext.Calldates.AddAsync(date);
                    CandidateCallDate candidateCall = new() { CallDateId = date.Id, CandidateId = CandidateId };
                    await _dbContext.CandidateCalldates.AddAsync(candidateCall);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private async Task<bool> AddTechnologyRelations(List<Technology> Technologies, Guid CandidateId)
        {
            try
            {
                foreach (Technology tech in Technologies)
                {
                    var existingTechnology = await _dbContext.Technologies
                        .Where(t => t.Title == tech.Title)
                        .SingleOrDefaultAsync();
                    if (existingTechnology != null)
                    {
                        CandidateTechnology candidateTechnology = new() { TechnologyId = existingTechnology.Id, CandidateId = CandidateId };
                        await _dbContext.CandidateTechnologies.AddAsync(candidateTechnology);
                        continue;
                    }

                    await _dbContext.Technologies.AddAsync(tech);
                    CandidateTechnology candidateTechnology1 = new() { TechnologyId = tech.Id, CandidateId = CandidateId };
                    await _dbContext.CandidateTechnologies.AddAsync(candidateTechnology1);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        private async Task<bool> RemoveCallDateRelations (Guid candidateId)
        {
            try
            {
                var candidateCallDates = await _dbContext.CandidateCalldates
                   .Where(cc => cc.CandidateId == candidateId)
                   .ToListAsync();
                candidateCallDates.ForEach(cc =>
                {
                    _dbContext.CandidateCalldates.Remove(cc);
                });
                return true;
            } catch
            {
                return false;
            }
            
        }

        private async Task<bool> RemoveTechnologyRelations(Guid candidateId)
        {
            try
            {
                var candidateTechnologies = await _dbContext.CandidateTechnologies
                .Where(ct => ct.CandidateId == candidateId)
                .ToListAsync();
                candidateTechnologies.ForEach(ct =>
                {
                    _dbContext.CandidateTechnologies.Remove(ct);
                });
                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> SaveToDbAsync ()
        {
            var saved = false;
            while (!saved)
            {
                try
                {
                    // Attempt to save changes to the database
                    await _dbContext.SaveChangesAsync();
                    saved = true;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    foreach (var entry in ex.Entries)
                    {
                        if (entry.Entity is Candidate)
                        {
                            var proposedValues = entry.CurrentValues;
                            var databaseValues = entry.GetDatabaseValues();

                            foreach (var property in proposedValues.Properties)
                            {
                                var proposedValue = proposedValues[property];
                                //var databaseValue = databaseValues[property];
                            }

                            // Refresh original values to bypass next concurrency check
                            entry.OriginalValues.SetValues(proposedValues);
                        }
                        else
                        {
                            throw new NotSupportedException(
                                "Don't know how to handle concurrency conflicts for "
                                + entry.Metadata.Name);
                        }
                    }
                }
            }
            return saved;
        }
    }
}
