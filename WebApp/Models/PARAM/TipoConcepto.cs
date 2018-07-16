using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.PARAM
{
    [Table("CORE.TiposConcepto")]
    public class TipoConcepto
    {
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public int Id { get; set; }

        [MaxLength(200, ErrorMessage = "El campo Descripcion no puede tener mas de 200 caracteres.")]
        public string Descripcion { get; set; }

    }
}
