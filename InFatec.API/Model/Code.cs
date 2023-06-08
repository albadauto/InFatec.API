using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InFatec.API.Model
{
    public class Code
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]  
        public string CodeString { get; set; }

        [ForeignKey("ApiLoginId")]
        public Login ApiLogin { get; set; }

        [NotMapped]
        public int ApiLoginId { get; set; } 

    }
}
