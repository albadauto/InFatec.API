using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InFatec.API.Model
{
    public class Events
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        [StringLength(255)]
        public string ImageName { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [NotMapped]
        [StringLength(50)]
        public IFormFile ImageFile { get; set; }

        public string? Image_Uri { get; set; }
    }
}
