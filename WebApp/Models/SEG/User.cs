using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.SEG
{
    [Table("seg.user")]
    public class User
    {
        
        public long Id { get; set; }

        [Required(ErrorMessage = "El campo usuario es obligatorio.")]
        [MaxLength(500, ErrorMessage = "El campo usuario no puede tener mas de 500 caracteres.")]
        public string Username { get; set; }

       
        [MaxLength(500, ErrorMessage = "El campo contraseña no puede tener mas de 500 caracteres.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El campo persona es obligatorio.")]
        public long PersonId { get; set; }

        public bool? Active { get; set; }

    }
}
