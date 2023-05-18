using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InFatec.API.Model
{
    public class Warnings
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Message { get; set; }

        [Required]
        public string ImgUri { get; set; }

        [Required]
        [StringLength(255)]
        public string ImageName { get; set; }

        public Login Login { get; set; }

        public int LoginId { get; set; }

        public string Title { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

    }
}