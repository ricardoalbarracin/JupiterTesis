using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.PARAM
{
    [Table("CORE.Rubros")]
    public class Rubro
    {
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public long Id { get; set; }

        [Required(ErrorMessage = "El campo Codigo es obligatorio.")]
        [MaxLength(10, ErrorMessage = "El campo Codigo no puede tener mas de 10 caracteres.")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "El campo Descripcion es obligatorio.")]
        public string Descripcion { get; set; }

    }

}
