using InFatec.API.Model;
using System.ComponentModel.DataAnnotations;

namespace InFatec.API.DTO
{
    public class CodeDTO
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public ApiLogin ApiLogin { get; set; }
    }
}
