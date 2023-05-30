namespace InFatec.API.DTO
{
    public class EditCoursesDTO
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Period { get; set; }
        public string Matter { get; set; }

        public string Floor { get; set; }

        public string? Coordinator { get; set; }

        public IFormFile Excel { get; set; }
    }
}
