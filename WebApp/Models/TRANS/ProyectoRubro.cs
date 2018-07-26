using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Utils;

namespace Core.Models.TRANS
{
    [Table("CORE.ProyectosRubros")]
    public class ProyectoRubro
    {
        [Required(ErrorMessage ="El campo Id es obligatorio.")]
        public int Id { get; set;}
     
        [Required(ErrorMessage ="El campo RubroId es obligatorio.")]
        public int RubroId { get; set;}
     
        [Required(ErrorMessage ="El campo ProyectoId es obligatorio.")]
        public int ProyectoId { get; set;}
     
        [Required(ErrorMessage ="El campo Valor es obligatorio.")]
        public int Valor { get; set;}
     
        [Required(ErrorMessage ="El campo Saldo es obligatorio.")]
        public int Saldo { get; set;}
     
        public int? UsuarioModificacion { get; set;}
     
        [Required(ErrorMessage ="El campo FechaCreacion es obligatorio.")]
        public DateTime FechaCreacion { get; set;}
     
        [Required(ErrorMessage ="El campo FechaModificacion es obligatorio.")]
        public DateTime FechaModificacion { get; set;}

       
     
    }
 
}
