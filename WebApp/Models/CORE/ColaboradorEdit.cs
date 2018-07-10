using Core.Models.ADMIN;
using Core.Models.CORE;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.CORE
{
    [Table("SEG.Usuarios")]
    public class ColaboradorEdit
    {
        public Persona Persona { get; set; }

        public List<Contrato> Contratos { get; set; }
    
    }
}
