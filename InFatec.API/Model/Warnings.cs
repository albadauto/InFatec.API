using System.ComponentModel.DataAnnotations;

namespace InFatec.API.Model
{
    public class Warnings
    {
        [Key]
        public int Id;

        [Required]
        [StringLength(255)]
        public string Message;

    }
}
