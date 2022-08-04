using System.ComponentModel.DataAnnotations;

namespace XplicityHRplatformBackEnd.Models
{
    public class Technology
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<Candidate> Candidates { get; set; } 
    }
}
