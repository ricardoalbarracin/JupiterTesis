using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.SEG
{
    [Table("seg.user")]
    public class UserEdit
    {
        public User User { get; set; }
        public List<Permission> Permissions { get; set; }
        public List<Role> Roles { get; set; }
    }
}
