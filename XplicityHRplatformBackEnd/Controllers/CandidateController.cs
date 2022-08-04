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
                new Candidate
                {
                    Id = 1,
                    FirstName = "my Name",
                    LastName = "my LastName",
                    LinkedIn = "this is LinkedIn",
                    Comment = "this is a Comment",
                    Technologies = "C#",
                    DateOfPastCalls = "random date",
                    DateOfFutureCall = "another future date",
                    OpenForSuggestions = true
                },
                new Candidate
                {
                    Id = 2,
                    FirstName = "my Nameeee",
                    LastName = "my LastName",
                    LinkedIn = "this is LinkedIn",
                    Comment = "this is a Comment",
                    Technologies = "C#",
                    DateOfPastCalls = "random date",
                    DateOfFutureCall = "another future date",
                    OpenForSuggestions = true
                }
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
            Console.WriteLine(newCandidate.DateOfPastCalls);
            Console.WriteLine(newCandidate.Comment);
            Console.WriteLine(newCandidate.LinkedIn);

            return Ok();
        }


    }
}
