using InFatec.API.Model;
using System.ComponentModel.DataAnnotations;

namespace InFatec.API.DTO
{
    public class CodeDTO
    {
        public int Id { get; set; }

        public string CodeString { get; set; }

        public int ApiLoginId { get; set; }
    }
}
