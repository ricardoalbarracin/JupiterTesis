using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.SEG
{
    [Table("SEG.Permisos")]
    public class Permiso
    {
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo Sigla es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo Sigla no puede tener mas de 100 caracteres.")]
        public string Sigla { get; set; }

        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Nombre { get; set; }

        public int? Activo { get; set; }

    }

    public class Permisos
    {
        public Permisos(List<Permiso> permisos)
        {
            ListPermisos = permisos;
        }

        public List<Permiso> ListPermisos { get; set; }
    }
}
