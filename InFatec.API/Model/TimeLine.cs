using System.ComponentModel.DataAnnotations;

namespace InFatec.API.Model
{
    public class TimeLine
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string ClassRoom { get; set; }

        [Required]
        [StringLength(50)]
        public Login Login { get; set; } //O login deve ser do tipo Professor

        [Required]
        public int Floor { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }
    }
}
