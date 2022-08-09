using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace XplicityHRplatformBackEnd.Models
{
    public class Candidate : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string LinkedIn { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;  
        public string DateOfFutureCall { get; set; } = string.Empty;
        public bool OpenForSuggestions { get; set; } = false;

        public Candidate() { }
        public Candidate(CandidateDto candidate)
        {
            Id = candidate.Id;
            FirstName = candidate.FirstName;
            LastName = candidate.LastName;
            LinkedIn =candidate.LinkedIn;
            Comment = candidate.Comment;
            DateOfFutureCall = candidate.DateOfFutureCall;
            OpenForSuggestions = candidate.OpenForSuggestions;
        }   
    }
}
