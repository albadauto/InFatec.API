﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InFatec.API.DTO
{
    public class WarningDTO
    {
        public string? ImageName { get; set; }
        public string Message { get; set; }

        public string? ImgUri { get; set; }

        public int LoginId { get; set; }
        public IFormFile ImageFile { get; set; }
    }
    
}
