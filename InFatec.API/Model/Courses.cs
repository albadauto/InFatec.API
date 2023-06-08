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
        public string Start { get; set; }

        [Required]
        public string End { get; set; }

        [Required]
        public string Matter { get; set; }

        [Required]
        public string Floor { get; set; }


        [Required]
        [StringLength(50)]
        public string Coordinator { get; set; }

        [NotMapped]
        public IFormFile? Excel { get; set; }
    }
}
