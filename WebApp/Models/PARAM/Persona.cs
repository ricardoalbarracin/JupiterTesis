using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.PARAM
{
    [Table("ADMIN.Personas")]
    public class Persona
    {
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo TipoDocumentoId es obligatorio.")]
        public int TipoDocumentoId { get; set; }

        [Required(ErrorMessage = "El campo Documento es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo Documento no puede tener mas de 50 caracteres.")]
        public string Documento { get; set; }

        public DateTime? FechaExpedicion { get; set; }

        [Required(ErrorMessage = "El campo PrimerNombre es obligatorio.")]
        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        [Required(ErrorMessage = "El campo PrimerApellido es obligatorio.")]
        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        [Required(ErrorMessage = "El campo FechaNacimiento es obligatorio.")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El campo SexoId es obligatorio.")]
        public int SexoId { get; set; }

        public int? DepartamentoNacimientoId { get; set; }

        public int? MunicipioNacimientoId { get; set; }

        public int? DepartamentoResidenciaId { get; set; }

        public int? MunicipioResidenciaId { get; set; }

        [MaxLength(50, ErrorMessage = "El campo Telefono no puede tener mas de 50 caracteres.")]
        public string Telefono { get; set; }

        [MaxLength(50, ErrorMessage = "El campo Celular no puede tener mas de 50 caracteres.")]
        public string Celular { get; set; }

        [Required(ErrorMessage = "El campo Correo es obligatorio.")]
        [MaxLength(500, ErrorMessage = "El campo Correo no puede tener mas de 500 caracteres.")]
        public string Correo { get; set; }

        [MaxLength(500, ErrorMessage = "El campo Direccion no puede tener mas de 500 caracteres.")]
        public string Direccion { get; set; }

        [Required(ErrorMessage = "El campo Cargo es obligatorio.")]
        public int CargoId { get; set; }

    }
}
