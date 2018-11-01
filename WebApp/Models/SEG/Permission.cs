using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.SEG
{
    [Table("seg.permission")]
    public class Permission
    {
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public long Id { get; set; }

        [Required(ErrorMessage = "El campo Sigla es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo Sigla no puede tener mas de 100 caracteres.")]
        public string Code { get; set; }

        public string FullDescription { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Description { get; set; }

        public int? Active { get; set; }

    }

    public class Permissions
    {
        public Permissions()
        {
            ListPermissions = new List<Permission>();
        }
        public Permissions(List<Permission> permissions)
        {
            ListPermissions = permissions;
        }

        public List<Permission> ListPermissions { get; set; }
    }
}
