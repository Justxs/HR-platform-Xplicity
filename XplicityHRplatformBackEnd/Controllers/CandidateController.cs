using Microsoft.AspNetCore.Mvc;

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


        [HttpPost(Name = "PostNewCandidate")]
        public IEnumerable<WeatherFort> Get()
        {
           
        }

    }
}
