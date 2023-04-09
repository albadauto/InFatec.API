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
        public int Floor { get; set; }

        public Login Login { get; set; }
        
        public int LoginId { get; set; }

        [Required]
        public TimeSpan Start { get; set; }

        [Required]
        public TimeSpan End { get; set; }
    }
}
