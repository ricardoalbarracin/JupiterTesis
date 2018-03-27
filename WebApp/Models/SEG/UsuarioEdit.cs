using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.SEG
{
    [Table("SEG.Usuarios")]
    public class UsuarioEdit
    {
        public Usuario Usuario { get; set; }
        public List<Permiso> Permisos { get; set; }
        public List<Role> Roles { get; set; }
    }
}
