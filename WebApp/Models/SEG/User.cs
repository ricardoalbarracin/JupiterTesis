using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.SEG
{
    [Table("seg.user")]
    public class User
    {
        [Required(ErrorMessage = "REQUIRED_ERROR_ID")]
        [Display(Name = "PROPERTY_NAME_ID")]
        public int Id { get; set; }


        [MaxLength(500, ErrorMessage = "LENGTH_ERROR_USERNAME")]
        [Required(ErrorMessage = "REQUIRED_ERROR_USERNAME")]
        [Display(Name = "PROPERTY_NAME_USERNAME")]
        public string Username { get; set; }


        [MaxLength(500, ErrorMessage = "LENGTH_ERROR_PASSWORD")]
        [Required(ErrorMessage = "REQUIRED_ERROR_PASSWORD")]
        [Display(Name = "PROPERTY_NAME_PASSWORD")]
        public string Password { get; set; }


        [Required(ErrorMessage = "REQUIRED_ERROR_PERSONID")]
        [Display(Name = "PROPERTY_NAME_PERSONID")]
        public int PersonId { get; set; }


        [Display(Name = "PROPERTY_NAME_ACTIVE")]
        public bool Active { get; set; }


    }
}
