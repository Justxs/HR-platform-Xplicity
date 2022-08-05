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
            List<Technology> technologyList = new List<Technology>();
            technologyList.Add(new Technology {Title= "C#", Id = 1 });
            List<Technology> technologyList2 = new List<Technology>();
            technologyList2.Add(new Technology { Title = "C++", Id = 2 });
            technologyList2.Add(new Technology { Title = "C#", Id = 1 });
            technologyList2.Add(new Technology { Title = "C", Id = 3 });

            List<CallDate> callDateList = new List<CallDate>();
            callDateList.Add(new CallDate { id = 1, date = "2017-05-06" });
            callDateList.Add(new CallDate { id = 1, date = "2015-04-06" });
            callDateList.Add(new CallDate { id = 1, date = "2015-04-06" });
            List<CallDate> callDateList2 = new List<CallDate>();
            callDateList2.Add(new CallDate { id = 1, date = "2015-09-01" });
            return new List<Candidate>
            {
                new Candidate
                {
                    Id = 1,
                    FirstName = "name",
                    LastName = "surname",
                    Technologies = technologyList,
                    LinkedIn = "LinkedIn",
                    Comment = "This is an epic comment",
                    OpenForSuggestions = true,
                    CallDates = callDateList,
                    DateOfFutureCall = "2025-07-03"

                },
                new Candidate
                {
                    Id = 1,
                    FirstName = "name",
                    LastName = "surname",
                    Technologies = technologyList2,
                    LinkedIn = "LinkedIn",
                    Comment = "This is an epic comment",
                    OpenForSuggestions = false,
                    CallDates = callDateList2,
                    DateOfFutureCall = "2025-07-03"
                }

            };
        }

        [HttpPost]
        public async Task<ActionResult<List<Candidate>>> CreateCandidate(Candidate newCandidate)
        {
            return Ok();
        }


    }
}
