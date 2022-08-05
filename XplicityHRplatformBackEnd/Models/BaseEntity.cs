using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace XplicityHRplatformBackEnd.Models
{
	public abstract class BaseEntity
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[JsonIgnore]
		public Guid Id { get; set; }

		[JsonIgnore]
		public DateTime Date { get; set; }

		protected BaseEntity()
		{
			Id = Guid.NewGuid();
			Date = DateTime.Now;
		}
	}

}
