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
               
            };
        }

        [HttpPost]
        public async Task<ActionResult<List<Candidate>>> CreateCandidate(Candidate newCandidate)
        {
            return Ok();
        }


    }
}
