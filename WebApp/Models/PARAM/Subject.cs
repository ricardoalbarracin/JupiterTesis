using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.PARAM
{
    [Table("core.subject")]
    public class Subject
    {
        [Required(ErrorMessage = "REQUIRED_ERROR_ID")]
        [Display(Name = "PROPERTY_NAME_ID")]
        public int Id { get; set; }


        [MaxLength(400, ErrorMessage = "LENGTH_ERROR_DESCRIPTION")]
        [Display(Name = "PROPERTY_NAME_DESCRIPTION")]
        public string Description { get; set; }


        [MaxLength(100, ErrorMessage = "LENGTH_ERROR_CODE")]
        [Required(ErrorMessage = "REQUIRED_ERROR_CODE")]
        [Display(Name = "PROPERTY_NAME_CODE")]
        public string Code { get; set; }


        [Required(ErrorMessage = "REQUIRED_ERROR_INSTITUTIONID")]
        [Display(Name = "PROPERTY_NAME_INSTITUTIONID")]
        public int InstitutionId { get; set; }

        [Display(Name = "PROPERTY_NAME_ACTIVE")]
        public bool Active { get; set; }

    }
}