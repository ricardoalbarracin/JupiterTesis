using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.PARAM
{
    [Table("param.localization_record_type")]
    public class LocalizationRecordType
    {
        [Required(ErrorMessage = "REQUIRED_ERROR_ID")]
        [Display(Name = "PROPERTY_NAME_ID")]
        public int Id { get; set; }


        [MaxLength(100, ErrorMessage = "LENGTH_ERROR_CODE")]
        [Required(ErrorMessage = "REQUIRED_ERROR_CODE")]
        [Display(Name = "PROPERTY_NAME_CODE")]
        public string Code { get; set; }


        [MaxLength(1000, ErrorMessage = "LENGTH_ERROR_DESCRIPTION")]
        [Required(ErrorMessage = "REQUIRED_ERROR_DESCRIPTION")]
        [Display(Name = "PROPERTY_NAME_DESCRIPTION")]
        public string Description { get; set; }


    }
}