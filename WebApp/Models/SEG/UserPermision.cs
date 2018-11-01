using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.SEG
{
    [Table("ser.user_permision")]
    public class UserPermision
    {
        public UserPermision(long userId, long permissionId)
        {
            UserId = userId;
            PermissionId = permissionId;
        }
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public long Id { get; set; }
        public long? UserId { get; set; }
        public long? PermissionId { get; set; }
    }
}
