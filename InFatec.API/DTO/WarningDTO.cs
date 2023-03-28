using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InFatec.API.DTO
{
    public class WarningDTO
    {
        public string Message { get; set; }

        public string? ImgUri { get; set; }

        public IFormFile ImageFile { get; set; }
    }
    
}
