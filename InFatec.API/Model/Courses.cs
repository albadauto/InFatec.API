using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InFatec.API.Model
{
    public class Courses
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]  
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Period { get; set; }

        [Required]
        public TimeSpan Start { get; set; }
        [Required]
        public TimeSpan End { get; set; }

        [Required]
        [StringLength(50)]
        public string Coordinator { get; set; }

        [NotMapped]
        public IFormFile Excel { get; set; }
    }
}
