using System.ComponentModel.DataAnnotations;

namespace InFatec.API.DTO
{
    public class CoursesDTO
    {
        public int? Id { get; set; }

        public string? Name { get; set; }
  
        public string? Period { get; set; }

        [System.ComponentModel.DataAnnotations.DataType(DataType.Time)]
        public TimeSpan? Start { get; set; } = null;

        [System.ComponentModel.DataAnnotations.DataType(DataType.Time)]
        public TimeSpan? End { get; set; } = null;

        public string? Coordinator { get; set; }

        public IFormFile Excel { get; set; }

    }
}
