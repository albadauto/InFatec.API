﻿using System.ComponentModel.DataAnnotations;

namespace InFatec.API.Model
{
    public class Warnings
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Message { get; set; }

        [Required]
        public string ImgUri { get; set; }

    }
}