using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InFatec.API.Model
{
    public class Login
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string RA { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(100)] 
        public string Password { get; set; }

        [Required]
        [StringLength(100)]
        public string Type { get; set; }

        [Required]
        [StringLength(100)]
        public string CPF { get; set; }


        
    }
}
