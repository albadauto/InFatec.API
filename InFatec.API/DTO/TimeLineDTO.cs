using System.ComponentModel.DataAnnotations;

namespace InFatec.API.DTO
{
    public class TimeLineDTO
    {

        public string ClassRoom { get; set; }

        public int Floor { get; set; }

        public int LoginId { get; set; }

        public string Start { get; set; }

        public string End { get; set; }

      
    }
}
