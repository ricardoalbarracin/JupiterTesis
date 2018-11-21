using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.PARAM
{
    [Table("Admin.Consecutivos")]
    public class Consecutivo
    {       
        public long Id { get; set; }
        public int Tipo { get; set; }
        public int valor { get; set; }        
    }
}
