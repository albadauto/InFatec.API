using InFatec.API.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InFatec.API.DTO
{
    public class EventsDTO
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public IFormFile ImageFile { get; set; }

        public string ImageName { get; set; }

        public string? Image_Uri { get; set; }
    }
}
