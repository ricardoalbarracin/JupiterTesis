using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.PARAM
{
    [Table("ADMIN.Personas")]
    public class Persona
    {
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public long Id { get; set; }

        [Required(ErrorMessage = "El campo Tipo documento es obligatorio.")]
        [Display(Name = "Tipo de documento")]
        public int TipoDocumentoId { get; set; }

        [Required(ErrorMessage = "El campo Documento es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo Documento no puede tener mas de 50 caracteres.")]
        [Display(Name = "Número de documento")]
        public string Documento { get; set; }

        [Required(ErrorMessage = "El campo Primer nombre es obligatorio.")]
        [Display(Name = "Primer nombre")]
        public string PrimerNombre { get; set; }

        [Display(Name = "Segundo nombre")]
        public string SegundoNombre { get; set; }

        [Required(ErrorMessage = "El campo Primer apellido es obligatorio.")]
        [Display(Name = "Primer apellido")]
        public string PrimerApellido { get; set; }

        [Display(Name = "Segundo apellido")]
        public string SegundoApellido { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [Required(ErrorMessage = "El campo Fecha de nacimiento es obligatorio.")]
        public DateTime FechaNacimiento { get; set; }

        [Display(Name = "Sexo")]
        [Required(ErrorMessage = "El campo Sexo es obligatorio.")]
        public int SexoId { get; set; }

        [Display(Name = "Departamento de nacimiento")]
        public int? DepartamentoNacimientoId { get; set; }

        [Display(Name ="Municipio de nacimiento")]
        public int? MunicipioNacimientoId { get; set; }

        [Display(Name = "Departamento de recidencia")]
        public int? DepartamentoResidenciaId { get; set; }

        [Display(Name = "Municipio de residencia")]
        public int? MunicipioResidenciaId { get; set; }

        [Display(Name = "Teléfono")]
        [MaxLength(50, ErrorMessage = "El campo Telefono no puede tener mas de 50 caracteres.")]
        public string Telefono { get; set; }

        [Display(Name = "Número de celular")]
        [MaxLength(50, ErrorMessage = "El campo Celular no puede tener mas de 50 caracteres.")]
        public string Celular { get; set; }

        [Display(Name = "Correo electrónico")]
        [Required(ErrorMessage = "El campo Correo es obligatorio.")]
        [MaxLength(500, ErrorMessage = "El campo Correo no puede tener mas de 500 caracteres.")]
        public string Correo { get; set; }

        [Display(Name = "Dirección")]
        [MaxLength(500, ErrorMessage = "El campo Direccion no puede tener mas de 500 caracteres.")]
        public string Direccion { get; set; }
        
        [Display(Name = "Cargo")]
        [Required(ErrorMessage = "El campo Cargo es obligatorio.")]
        public int CargoId { get; set; }

        [Display(Name = "Dependencia")]
        [Required(ErrorMessage = "El campo Dependencia es obligatorio.")]
        public int DependenciaId { get; set; }

    }
}
