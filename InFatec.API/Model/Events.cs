using System.ComponentModel.DataAnnotations;

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
        public string Description { get; set; }


    }
}
