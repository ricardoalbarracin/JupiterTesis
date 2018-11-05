using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.PARAM
{
    [Table("admin.gender")]
    public class Gender
    {      
        public int Id { get; set; }
        
        public string Description { get; set; }
    }
}
