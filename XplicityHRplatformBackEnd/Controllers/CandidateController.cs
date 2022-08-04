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
                }
            };
        }

       


    }
}
