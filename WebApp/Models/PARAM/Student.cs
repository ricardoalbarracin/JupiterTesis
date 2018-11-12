using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.PARAM
{
    [Table("core.student")]
    public class Student
    {
        [Required(ErrorMessage = "REQUIRED_ERROR_ID")]
        [Display(Name = "PROPERTY_NAME_ID")]
        public int Id { get; set; }


        [Required(ErrorMessage = "REQUIRED_ERROR_PERSONID")]
        [Display(Name = "PROPERTY_NAME_PERSONID")]
        public int PersonId { get; set; }


        [Required(ErrorMessage = "REQUIRED_ERROR_INSTITUTIONID")]
        [Display(Name = "PROPERTY_NAME_INSTITUTIONID")]
        public int InstitutionId { get; set; }


        [Display(Name = "PROPERTY_NAME_ACADEMICGRADEID")]
        public int? AcademicGradeId { get; set; }


    }
}
