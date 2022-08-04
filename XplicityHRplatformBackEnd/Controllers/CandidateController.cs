using Microsoft.AspNetCore.Mvc;
using XplicityHRplatformBackEnd.Models;

namespace XplicityHRplatformBackEnd.Controllers
{
    [Route("api/candidates")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ILogger<CandidateController> _logger;
        
        public CandidateController(ILogger<CandidateController> logger)
        {
            _logger = logger;
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
