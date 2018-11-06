using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.PARAM
{
    [Table("param.document_type")]
    public class DocumentType
    {
        [Required(ErrorMessage = "REQUIRED_ERROR_ID")]
        [Display(Name = "PROPERTY_NAME_ID")]
        public int Id { get; set; }


        [MaxLength(50, ErrorMessage = "LENGTH_ERROR_DESCRIPTION")]
        [Required(ErrorMessage = "REQUIRED_ERROR_DESCRIPTION")]
        [Display(Name = "PROPERTY_NAME_DESCRIPTION")]
        public string Description { get; set; }


        [MaxLength(10, ErrorMessage = "LENGTH_ERROR_CODE")]
        [Required(ErrorMessage = "REQUIRED_ERROR_CODE")]
        [Display(Name = "PROPERTY_NAME_CODE")]
        public string Code { get; set; }


    }
}