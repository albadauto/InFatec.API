using System.ComponentModel.DataAnnotations;

namespace InFatec.API.DTO
{
    public class ApiLoginDTO
    {
        public int Id { get; set; }

  
        public string Username { get; set; }


        public string Password { get; set; }
    }
}
