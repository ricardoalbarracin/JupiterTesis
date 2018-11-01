using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.SEG
{
    [Table("seg.role")]
    public class Role
    {
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public long Id { get; set; }

        [MaxLength(100, ErrorMessage = "El campo Sigla no puede tener mas de 100 caracteres.")]
        public string Code { get; set; }

        public string FullDescription { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Description { get; set; }

        public int? Active { get; set; }

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
