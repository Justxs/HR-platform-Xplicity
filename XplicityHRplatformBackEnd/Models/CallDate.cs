using System.ComponentModel.DataAnnotations;

namespace XplicityHRplatformBackEnd.Models
{
    public class CallDate : BaseEntity
    {
        public string DateOfCall { set; get; } = string.Empty;
    }
}
