using Microsoft.AspNetCore.Mvc;
using XplicityHRplatformBackEnd.Models;

namespace XplicityHRplatformBackEnd.Controllers
{
    [Route("api/candidates")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Candidate>>> GetCandidates()
        {
            return new List<Candidate>
            {
                //snew Candidate
                //s{
                //s    Id = 1,
                //s    FirstName = "my Name",
                //s    LastName = "my LastName",
                //s    LinkedIn = "this is LinkedIn",
                //s    Comment = "this is a Comment",
                //s    Technologies = {"C#" },
                //s    DatesOfPastCalls = {"random date" },
                //s    DateOfFutureCall = "another future date",
                //s    OpenForSuggestions = true
                //s},
                //snew Candidate
                //s{
                //s    Id = 2,
                //s    FirstName = "my Name",
                //s    LastName = "my LastName",
                //s    LinkedIn = "this is LinkedIn",
                //s    Comment = "this is a Comment",
                //s    Technologies = "C#",
                //s    DateOfPastCalls = "random date",
                //s    DateOfFutureCall = "another future date",
                //s    OpenForSuggestions = true
                //s},
                //snew Candidate
                //s{
                //s    Id = 2,
                //s    FirstName = "my Name",
                //s    LastName = "my LastName",
                //s    LinkedIn = "this is LinkedIn",
                //s    Comment = "this is a Comment",
                //s    Technologies = "C#",
                //s    DateOfPastCalls = "random date",
                //s    DateOfFutureCall = "another future date",
                //s    OpenForSuggestions = true
                //s},
                //snew Candidate
                //s{
                //s    Id = 2,
                //s    FirstName = "my Name",
                //s    LastName = "my LastName",
                //s    LinkedIn = "this is LinkedIn",
                //s    Comment = "this is a Comment",
                //s    Technologies = "C#",
                //s    DateOfPastCalls = "random date",
                //s    DateOfFutureCall = "another future date",
                //s    OpenForSuggestions = true
                //s},
                //snew Candidate
                //s{
                //s    Id = 2,
                //s    FirstName = "my Name",
                //s    LastName = "my LastName",
                //s    LinkedIn = "this is LinkedIn",
                //s    Comment = "this is a Comment",
                //s    Technologies = "C#",
                //s    DateOfPastCalls = "random date",
                //s    DateOfFutureCall = "another future date",
                //s    OpenForSuggestions = true
                //s}
            };
        }



        [HttpPost]
        public async Task<ActionResult<List<Candidate>>> CreateCandidate(Candidate newCandidate)
        {
            Console.WriteLine(newCandidate.Id);
            Console.WriteLine(newCandidate.Comment);
            Console.WriteLine(newCandidate.FirstName);
            Console.WriteLine(newCandidate.LastName);
            Console.WriteLine(newCandidate.DateOfFutureCall);
            Console.WriteLine(newCandidate.DatesOfPastCalls[0]);
            Console.WriteLine(newCandidate.DatesOfPastCalls[1]);    
            Console.WriteLine(newCandidate.Comment);
            Console.WriteLine(newCandidate.Technologies[0]);
            Console.WriteLine(newCandidate.Technologies[1]);
            Console.WriteLine(newCandidate.LinkedIn);

            return Ok();
        }


    }
}
