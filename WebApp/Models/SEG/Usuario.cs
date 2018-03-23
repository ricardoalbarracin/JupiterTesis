using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.SEG
{
    [Table("SEG.Usuarios")]
    public class Usuario
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Username { get; set; }

        [Required]
        [MaxLength(500)]
        public string Password { get; set; }

        public int? PersonaId { get; set; }

        public bool? Activo { get; set; }

    }
}
