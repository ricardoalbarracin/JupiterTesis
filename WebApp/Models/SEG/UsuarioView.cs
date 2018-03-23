using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.SEG
{
    public class UsuarioView
    {
        public int Id { get; set; }

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
        public bool Activo { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public IList<string> Permisos { get; set; }
        public IList<string> Roles { get; set; }
    }
}
