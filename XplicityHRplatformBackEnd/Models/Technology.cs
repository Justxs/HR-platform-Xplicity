using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace XplicityHRplatformBackEnd.Models
{
    public class Technology
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
