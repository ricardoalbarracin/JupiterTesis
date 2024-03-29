﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.SEG
{
    [Table("seg.role")]
    public class Role
    {
        [Required(ErrorMessage = "REQUIRED_ERROR_ID")]
        [Display(Name = "PROPERTY_NAME_ID")]
        public int Id { get; set; }


        [MaxLength(100, ErrorMessage = "LENGTH_ERROR_CODE")]
        [Display(Name = "PROPERTY_NAME_CODE")]
        public string Code { get; set; }


        [MaxLength(1000, ErrorMessage = "LENGTH_ERROR_FULLDESCRIPTION")]
        [Display(Name = "PROPERTY_NAME_FULLDESCRIPTION")]
        public string FullDescription { get; set; }


        [MaxLength(1000, ErrorMessage = "LENGTH_ERROR_DESCRIPTION")]
        [Required(ErrorMessage = "REQUIRED_ERROR_DESCRIPTION")]
        [Display(Name = "PROPERTY_NAME_DESCRIPTION")]
        public string Description { get; set; }

        [Display(Name = "PROPERTY_NAME_ACTIVE")]
        public int? Active { get; set; }

    }

    public class Roles
    {
        public Roles()
        {
            ListRoles = new List<Role>();
        }

        public Roles(List<Role> roles)
        {
            ListRoles = roles;
        }
        public List<Role> ListRoles { get; set; }
    }
}
