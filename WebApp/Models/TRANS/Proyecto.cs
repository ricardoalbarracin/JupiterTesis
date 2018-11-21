using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models.TRANS
{
    [Table("CORE.Proyectos")]
    public class Proyecto
    {
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public long Id { get; set; }

        [Required(ErrorMessage = "El campo Código es obligatorio.")]
        [MaxLength(10, ErrorMessage = "El campo Código no puede tener mas de 10 caracteres.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El campo Descripción es obligatorio.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo Dependencia es obligatorio.")]
        [DisplayName("Dependencia")]
        public int DependenciaId { get; set; }

        [Required(ErrorMessage = "El campo presupuesto asignado es obligatorio.")]
        [DisplayName("Presupuesto")]
        public float PresupuestoAsignado { get; set; }

        [DisplayName("Fecha de creación")]
        public DateTime? FechaCreacion { get; set; }

        public int UsuarioCreacion { get; set; }

        [DisplayName("Lider de proyecto")]
        public int? ColaboradorLiderId { get; set; }

        
        [DisplayName("Presupuesto sin asignar ")]
        [Write(false)]
        public float PresupuestosinAsignar { get; set; }

    }
}
