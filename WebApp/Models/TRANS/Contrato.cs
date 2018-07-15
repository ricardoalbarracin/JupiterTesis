using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.TRANS
{
    [Table("CORE.Contratos")]
    public class Contrato
    {
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo PersonaId es obligatorio.")]
        public int PersonaId { get; set; }

        [Required(ErrorMessage = "El campo CargoId es obligatorio.")]
        public int CargoId { get; set; }

        [Required(ErrorMessage = "El campo FechaContratacion es obligatorio.")]
        public DateTime FechaContratacion { get; set; }

        public DateTime? FechaTerminacionContrato { get; set; }

        [Required(ErrorMessage = "El campo Estado es obligatorio.")]
        [MaxLength(10, ErrorMessage = "El campo Estado no puede tener mas de 10 caracteres.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "El campo CodigoContrato es obligatorio.")]
        public string CodigoContrato { get; set; }

    }
}
