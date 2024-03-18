﻿using System.ComponentModel.DataAnnotations;

namespace PROJET.Model
{
    public class SocioProfessionalCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }

    }
}
