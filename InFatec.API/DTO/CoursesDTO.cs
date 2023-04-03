namespace InFatec.API.DTO
{
    public class CoursesDTO
    {
        public int? Id { get; set; }


        public string? Name { get; set; }

  
        public string? Period { get; set; }

        public TimeSpan? Start { get; set; }
        public TimeSpan? End { get; set; }

        public string? Coordinator { get; set; }

        public IFormFile Excel { get; set; }

    }
}
