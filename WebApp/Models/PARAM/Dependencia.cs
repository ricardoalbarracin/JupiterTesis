using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.PARAM
{
    [Table("CORE.Dependencias")]
    public class Dependencia
    {
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public long Id { get; set; }

        [Required(ErrorMessage = "El campo código es obligatorio.")]
        [MaxLength(10, ErrorMessage = "El campo código no puede tener más de 10 caracteres.")]
        [Display(Name = "Código")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El campo descripción es obligatorio.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "El campo Colaborador líder es obligatorio.")]
        [Display(Name = "Colaborador líder")]
        public int ColaboradorLiderId { get; set; }

        public int? PadreId { get; set; }

    }
}
