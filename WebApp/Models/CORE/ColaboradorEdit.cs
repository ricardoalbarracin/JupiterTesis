using Core.Models.ADMIN;
using Core.Models.CORE;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.CORE
{
    [Table("SEG.Usuarios")]
    public class ColaboradorEdit
    {
        public Persona Persona { get; set; }

        public Colaborador Colaborador { get; set; }

    }
}
