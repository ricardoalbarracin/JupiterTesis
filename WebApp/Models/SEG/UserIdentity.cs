using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Core.Models.SEG
{
    public class UserIdentity
    {
        public long Id { get; set; }

        private string _username = "";

        [Required]
        [Display(Name = "Usuario")]
        public string Username
        {
            get { return _username.ToUpper(); }
            set { _username = value; }
        }
        
        [Required]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }
        
        public bool Active { get; set; }

        public long? PersonId { get; set; }

        public string Document { get; set; }
        
        public string FirtsName { get; set; }
        
        public string SecondName { get; set; }
        
        public string Surname { get; set; }

        public string SecondSurname { get; set; }
        
        public DateTime? BirthDate { get; set; }
        
        public IList<Permission> Permissions { get; set; }
        
        public IList<Role> Roles { get; set; }

        public string Correo { get; set; }
    }
}
