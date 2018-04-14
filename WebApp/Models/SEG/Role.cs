using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.SEG
{
    [Table("SEG.Roles")]
    public class Role
    {
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public int Id { get; set; }

        [MaxLength(100, ErrorMessage = "El campo Sigla no puede tener mas de 100 caracteres.")]
        public string Sigla { get; set; }

        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Nombre { get; set; }

        public int? Activo { get; set; }

    }

    public class Roles
    {
        public Roles()
        {
            ListRoles = new List<Role>();
        }

        public Roles(List<Role> roles)
        {
            ListRoles = roles;
        }
        public List<Role> ListRoles { get; set; }
    }
}
