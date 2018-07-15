using Core.Models.PARAM;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.TRANS
{
    [Table("SEG.Usuarios")]
    public class ColaboradorEdit
    {
        public Persona Persona { get; set; }

        public List<Contrato> Contratos { get; set; }
    
    }
}
