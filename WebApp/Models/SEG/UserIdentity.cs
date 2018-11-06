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
        [Display(Name = "PROPERTY_NAME_USERNAME")]
        public string Username
        {
            get { return _username.ToUpper(); }
            set { _username = value; }
        }
        
        [Required]
        [Display(Name = "PROPERTY_NAME_PASSWORD")]
        public string Password { get; set; }

        [Display(Name = "PROPERTY_NAME_ACTIVE")]
        public bool Active { get; set; }

        [Display(Name = "PROPERTY_NAME_PERSONID")]
        public long? PersonId { get; set; }

        [Display(Name = "PROPERTY_NAME_DOCUMENT")]
        public string Document { get; set; }

        [Display(Name = "PROPERTY_NAME_FIRST")]
        public string FirtsName { get; set; }

        [Display(Name = "PROPERTY_NAME_SECONDNAME")]
        public string SecondName { get; set; }

        [Display(Name = "PROPERTY_NAME_SURNAME")]
        public string Surname { get; set; }

        [Display(Name = "PROPERTY_NAME_SECONDSURNAME")]
        public string SecondSurname { get; set; }

        [Display(Name = "PROPERTY_NAME_BIRTHDATE")]
        public DateTime? BirthDate { get; set; }


        public IList<Permission> Permissions { get; set; }
        
        public IList<Role> Roles { get; set; }

        public string Correo { get; set; }
    }
}
