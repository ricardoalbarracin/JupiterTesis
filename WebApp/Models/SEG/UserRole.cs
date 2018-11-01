using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.SEG
{
    [Table("seg.user_role")]
    public class UserRole
    {
        public UserRole(long? userId, long roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public long Id { get; set; }
        public long? UserId { get; set; }
        public long? RoleId { get; set; }
    
    }
}
