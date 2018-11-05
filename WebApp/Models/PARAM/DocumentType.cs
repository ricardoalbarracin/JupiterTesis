using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.PARAM
{
    [Table("admin.document_type")]
    public class DocumentType
    {
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public int Id { get; set; }

        [MaxLength(200, ErrorMessage = "El campo Nombre no puede tener mas de 200 caracteres.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El campo Sigla es obligatorio.")]
        public string Code { get; set; }

    }
}
