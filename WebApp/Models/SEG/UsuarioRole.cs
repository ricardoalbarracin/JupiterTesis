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
        public UsuarioRole(int? usuarioId, int roleId)
        {
            UsuarioId = usuarioId;
            RoleId = roleId;
        }

        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public int Id { get; set; }
        public int? UsuarioId { get; set; }
        public int? RoleId { get; set; }
    
    }
}
