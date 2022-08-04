using System.ComponentModel.DataAnnotations;

namespace XplicityHRplatformBackEnd.Models
{
    public class CallDate
    {
        [Key]
        public int id { get; set; }

        public string date { set; get; } = string.Empty;

        public List<Candidate> Candidates { get; set; }
    }
}
