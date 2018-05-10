using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.SEG
{
    [Table("SEG.UsuariosRoles")]
    public class UsuarioRole
    {
        public UsuarioRole(long? usuarioId, long roleId)
        {
            UsuarioId = usuarioId;
            RoleId = roleId;
        }

        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public long Id { get; set; }
        public long? UsuarioId { get; set; }
        public long? RoleId { get; set; }
    
    }
}
