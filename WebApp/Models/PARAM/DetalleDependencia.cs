using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.PARAM
{
    
    public class DetalleDependencia
    {
        public int Id { get; set; }

        public int PadreId { get; set; }

        public string Codigo { get; set; }

        public string Descripcion { get; set; }

        
        public string Colaborador { get; set; }

       

    }
}
