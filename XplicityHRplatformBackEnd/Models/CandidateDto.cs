using System.ComponentModel.DataAnnotations;

namespace XplicityHRplatformBackEnd.Models
{
    public class CandidateDto : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string LinkedIn { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public List<CallDate> PastCallDates { get; set; } = new List<CallDate> { };
        public List<Technology> Technologies { get; set; } = new List<Technology> { };
        public string DateOfFutureCall { get; set; } = string.Empty;
        public bool OpenForSuggestions { get; set; } = false;

        public CandidateDto() { }
        public CandidateDto( List<CallDate> pastCallDates, List<Technology> technologies, Candidate candidate)
        {
            Id = candidate.Id;      
            FirstName = candidate.FirstName;
            LastName = candidate.LastName;
            LinkedIn = candidate.LinkedIn;
            Comment = candidate.Comment;
            PastCallDates = pastCallDates;
            Technologies = technologies;
            DateOfFutureCall = candidate.DateOfFutureCall;
            OpenForSuggestions = candidate.OpenForSuggestions;
        }   
    }
}
