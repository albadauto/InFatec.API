using System.ComponentModel.DataAnnotations;

namespace InFatec.API.DTO
{
    public class CoursesDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }
  
        public string? Period { get; set; }
        public string Matter { get; set; }
        public string Floor { get; set; }

        public string Start { get; set; } 

        public string End { get; set; }

        public string? Coordinator { get; set; }

        public IFormFile? Excel { get; set; }

    }
}
