using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.PARAM
{
    
    public class StudentEdit
    {
        public Person Person { get; set; }

        public Student Student { get; set; }

    }
}