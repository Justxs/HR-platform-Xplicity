using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace XplicityHRplatformBackEnd.Models
{
    public class Technology : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
    }
}
