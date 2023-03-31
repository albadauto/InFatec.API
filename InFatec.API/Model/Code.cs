using System.ComponentModel.DataAnnotations;

namespace InFatec.API.Model
{
    public class Code
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]  
        public string CodeString { get; set; }

        public Login ApiLogin { get; set; }

    }
}
