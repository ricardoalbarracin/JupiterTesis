using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Utils;

namespace Core.Models.TRANS
{
    [Table("CORE.ProyectosRubros")]
    public class ProyectoRubro
    {
        [Required(ErrorMessage ="El campo Id es obligatorio.")]
        public long Id { get; set;}
     
        [Required(ErrorMessage ="El campo Tipo de rubro es obligatorio.")]
        [DisplayName("Tipo de rubro")]
        public int RubroId { get; set;}
     
        [Required(ErrorMessage ="El campo Proyecto es obligatorio.")]
        [DisplayName("Proyecto")]
        public int ProyectoId { get; set;}
     
        [Required(ErrorMessage ="El campo Valor es obligatorio.")]
        public int Valor { get; set;}
     
        [Required(ErrorMessage ="El campo Saldo es obligatorio.")]
        public int? Saldo { get; set;}
     
        public int? UsuarioModificacion { get; set;}
     
        [Required(ErrorMessage ="El campo Fecha de creación es obligatorio.")]
        [DisplayName("Fecha de creación")]
        public DateTime FechaCreacion { get; set;}
     
        [Required(ErrorMessage ="El campo Fecha de Modificación es obligatorio.")]
        [DisplayName("Fecha de modificación")]
        public DateTime FechaModificacion { get; set;}

       

    }
 
}
