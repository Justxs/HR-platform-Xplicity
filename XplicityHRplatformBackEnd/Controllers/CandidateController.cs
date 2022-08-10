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

                foreach (Technology tech in request.Technologies)
                {
                    CandidateTechnology candidateTechnology = new() { TechnologyId = tech.Id, CandidateId = newCandidateId };
                    Guid candidateCalldateId = await _dataUtilities.AddEntry(_dbContext.CandidateTechnologies, candidateTechnology);
                }
            }
            return Ok();
        }

        [HttpPut]
        public async Task<StatusCodeResult> UpdateCandidate([FromBody] CandidateDto request)
        {
            Candidate updatedCandidate = new (request);
            var candidateCallDates = await _dbContext.CandidateCalldates
                .Where(cc => cc.CandidateId == request.Id)
                .ToListAsync();
            candidateCallDates.ForEach(cc =>
            {
                _dbContext.CandidateCalldates.Remove(cc);
            });

            var candidateTechnologies = _dbContext.CandidateTechnologies
                .Where(ct => ct.CandidateId == request.Id)
                .ToList();
            candidateTechnologies.ForEach(ct =>
            {
                _dbContext.CandidateTechnologies.Remove(ct);
            });

            _dbContext.Candidates.Update(updatedCandidate);

            foreach (CallDate date in request.PastCallDates)
            {
                var callDate = await _dbContext.Calldates
                    .Where(c => c.DateOfCall == date.DateOfCall)
                    .SingleOrDefaultAsync();
                if (callDate != null)
                { 
                    CandidateCallDate candidateCallDate = new() { CallDateId = callDate.Id, CandidateId = updatedCandidate.Id };
                    await _dbContext.CandidateCalldates.AddAsync(candidateCallDate);
                    continue;
                }

                await _dbContext.Calldates.AddAsync(date);
                var newCallDateId = date.Id;

                CandidateCallDate candidateCall = new() { CallDateId = newCallDateId, CandidateId = updatedCandidate.Id };
                await _dbContext.CandidateCalldates.AddAsync(candidateCall);
            }

            foreach (Technology tech in request.Technologies)
            {
                CandidateTechnology candidateTechnology = new() { TechnologyId = tech.Id, CandidateId = updatedCandidate.Id };
                await _dbContext.CandidateTechnologies.AddAsync(candidateTechnology);
            }
            

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
                                var databaseValue = databaseValues[property];
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
            return Ok();
        }


        [HttpDelete]
        [Route("{IdToRemove}")]
        public async Task<StatusCodeResult> DeleteCandidate([FromRoute] Guid IdToRemove)
        {
            Candidate candidate = new() { Id = IdToRemove };
           
            var candidateCallDates = await _dbContext.CandidateCalldates
                .Where(cc => cc.CandidateId == IdToRemove)
                .ToListAsync();
            candidateCallDates.ForEach( cc =>
            {
                _dbContext.CandidateCalldates.Remove(cc);
            });

            var candidateTechnologies = _dbContext.CandidateTechnologies
                .Where(ct => ct.CandidateId == IdToRemove)
                .ToList();
            candidateTechnologies.ForEach( ct =>
            {
                _dbContext.CandidateTechnologies.Remove(ct);
            });

            _dbContext.Candidates.Remove(candidate);

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
                                var databaseValue = databaseValues[property];
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
            return Ok();
        }
    }
}
