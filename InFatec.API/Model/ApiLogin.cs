using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace InFatec.API.Model
{
    public class ApiLogin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)] 
        public string Password { get; set; }    
    }
}
