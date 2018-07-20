using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.TRANS
{
    [Table("CORE.Proyectos")]
    public class Proyecto
    {
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public long Id { get; set; }

        [Required(ErrorMessage = "El campo Codigo es obligatorio.")]
        [MaxLength(10, ErrorMessage = "El campo Codigo no puede tener mas de 10 caracteres.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El campo Descripcion es obligatorio.")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo DependenciaId es obligatorio.")]
        [DisplayName("Dependencia")]
        public int DependenciaId { get; set; }

        [Required(ErrorMessage = "El campo presupueto asignado es obligatorio.")]
        [DisplayName("Presupuesto asignado")]
        public decimal PresupuestoAsignado { get; set; }

        [DisplayName("Fecha de creacion")]
        public DateTime? FechaCreacion { get; set; }

        public DateTime? UsuarioCreacion { get; set; }

        [DisplayName("Lider de proyecto")]
        public int? ColaboradorLiderId { get; set; }

    }
}
