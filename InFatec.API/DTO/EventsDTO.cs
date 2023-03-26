using System.ComponentModel.DataAnnotations;

namespace InFatec.API.DTO
{
    public class EventsDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
