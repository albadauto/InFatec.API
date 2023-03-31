using System.ComponentModel.DataAnnotations;

namespace InFatec.API.DTO
{
    public class ApiLoginDTO
    {
        public string RA { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string CPF { get; set; }

        public int Type { get; set; }

    }
}
