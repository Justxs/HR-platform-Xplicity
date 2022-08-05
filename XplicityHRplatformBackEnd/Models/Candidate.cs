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
        [JsonIgnore]
        //public List<CallDate> CAget; set; }

    }
}
