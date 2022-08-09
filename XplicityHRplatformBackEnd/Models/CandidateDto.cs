using System.ComponentModel.DataAnnotations;

namespace XplicityHRplatformBackEnd.Models
{
    public class CandidateDto : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string LinkedIn { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public CallDate[] PastCallDates { get; set; } = new CallDate[] { };
        public Technology[] Technologies { get; set; } = new Technology[] { };
        public string DateOfFutureCall { get; set; } = String.Empty;
        public bool OpenForSuggestions { get; set; } = false;
    }
}
