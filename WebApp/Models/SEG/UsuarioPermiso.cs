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
        public UsuarioPermiso(long usuarioId, long permisoId)
        {
            UsuarioId = usuarioId;
            PermisoId = permisoId;
        }
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public long Id { get; set; }
        public long? UsuarioId { get; set; }
        public long? PermisoId { get; set; }
    }
}
