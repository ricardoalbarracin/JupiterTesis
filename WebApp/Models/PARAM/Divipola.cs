using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.PARAM
{
    [Table("ADMIN.DIVIPOLA")]
    public class Divipola
    {        
        public long Id { get; set; }            
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public long PadreId { get; set; }
        public long TDivipolaId { get; set; }


    }
}
