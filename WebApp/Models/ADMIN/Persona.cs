using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Models.ADMIN
{
    [Table("ADMIN.Personas")]
    public class Persona
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int TipoDocumentoId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Documento { get; set; }

        public DateTime? FechaExpedicion { get; set; }

        [Required]
        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        [Required]
        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public int SexoId { get; set; }

        public int? DepartamentoNacimientoId { get; set; }

        public int? MunicipioNacimientoId { get; set; }

        public int? DepartamentoResidenciaId { get; set; }

        public int? MunicipioResidenciaId { get; set; }

        [MaxLength(50)]
        public string Telefono { get; set; }

        [MaxLength(50)]
        public string Celular { get; set; }

        [Required]
        [MaxLength(500)]
        public string Correo { get; set; }

        [MaxLength(500)]
        public string Direccion { get; set; }

    }
}
