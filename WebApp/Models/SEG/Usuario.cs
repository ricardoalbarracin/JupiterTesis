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
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo usuario es obligatorio.")]
        [MaxLength(500, ErrorMessage = "El campo usuario no puede tener mas de 500 caracteres.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "El campo contraseña es obligatorio.")]
        [MaxLength(500, ErrorMessage = "El campo contraseña no puede tener mas de 500 caracteres.")]
        public string Password { get; set; }

        public int? PersonaId { get; set; }

        public bool? Activo { get; set; }

    }
}
