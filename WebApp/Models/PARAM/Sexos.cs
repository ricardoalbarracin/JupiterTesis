using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.PARAM
{
    [Table("ADMIN.Sexos")]
    public class Sexos
    {      
        public int Id { get; set; }
        
        public string Nombre { get; set; }
    }
}
