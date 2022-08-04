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
            string[] arr = {"C#"};
            return new List<Candidate>
            {
                new Candidate
                { 
                    Id = 1,
                    FirstName = "my Name",
                    LastName = "my LastName",
                    LinkedIn = "this is LinkedIn",
                    Comment = "this is a Comment",
                    Technologies = arr,
                    DatesOfPastCalls = {},
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
                    Technologies = {},
                    DatesOfPastCalls = {},
                    DateOfFutureCall = "another future date",
                    OpenForSuggestions = false
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
