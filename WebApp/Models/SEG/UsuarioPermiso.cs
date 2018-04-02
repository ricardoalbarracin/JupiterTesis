using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.SEG
{
    [Table("SEG.UsuariosPermisos")]
    public class UsuarioPermiso
    {
        public UsuarioPermiso(int usuarioId, int permisoId)
        {
            UsuarioId = usuarioId;
            PermisoId = permisoId;
        }
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public int Id { get; set; }
        public int? UsuarioId { get; set; }
        public int? PermisoId { get; set; }
    }
}
