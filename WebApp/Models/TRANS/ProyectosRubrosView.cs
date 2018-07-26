using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Core.Models.Utils;

namespace Core.Models.TRANS
{
   
    public class ProyectosRubrosView: ProyectoRubro
    {
        public string Proyecto { get; set; }

        public string Codigo { get; set; }

        public string Descripcion { get; set; }

        public OperationType OperationType { get; set; }
     
     
    }
 
}
