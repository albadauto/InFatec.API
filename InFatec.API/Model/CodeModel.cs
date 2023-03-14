using System.ComponentModel.DataAnnotations;

namespace InFatec.API.Model
{
    public class CodeModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]  
        public string Code { get; set; }

        public ApiLogin ApiLogin { get; set; }
    }
}
