using InFatec.API.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InFatec.API.DTO
{
    public class ApiLoginDTO
    {

        public string RA { get; set; }

   
        public string Name { get; set; }


        public string Email { get; set; }

  
        public string Password { get; set; }

        /**
            * 1- Professor
            * 2- Administrador
         */

        public int Type { get; set; }


        public int CoursesId { get; set; }


    }
}
