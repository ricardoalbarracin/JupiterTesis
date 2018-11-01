using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.PARAM
{
    [Table("ADMIN.Personas")]
    public class Person
    {
        [Required(ErrorMessage = "El campo Id es obligatorio.")]
        public long Id { get; set; }

        [Required(ErrorMessage = "El campo TipoDocumentoId es obligatorio.")]
        public int DocumentType { get; set; }

        [Required(ErrorMessage = "El campo Documento es obligatorio.")]
        [MaxLength(50, ErrorMessage = "El campo Documento no puede tener mas de 50 caracteres.")]
        public string Document { get; set; }

        public DateTime? ExpeditionDate { get; set; }

        [Required(ErrorMessage = "El campo PrimerNombre es obligatorio.")]
        public string FirtsName { get; set; }

        public string SecondName { get; set; }

        [Required(ErrorMessage = "El campo PrimerApellido es obligatorio.")]
        public string Surname { get; set; }

        public string SecondSurname { get; set; }

        [Required(ErrorMessage = "El campo FechaNacimiento es obligatorio.")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "El campo SexoId es obligatorio.")]
        public int GenderId { get; set; }

        public int? PlaceBirthId { get; set; }

        public int? PlaceResidenceId { get; set; }

        [MaxLength(50, ErrorMessage = "El campo Telefono no puede tener mas de 50 caracteres.")]
        public string Phone { get; set; }

        [MaxLength(50, ErrorMessage = "El campo Celular no puede tener mas de 50 caracteres.")]
        public string Mobile { get; set; }

        [Required(ErrorMessage = "El campo Correo es obligatorio.")]
        [MaxLength(500, ErrorMessage = "El campo Correo no puede tener mas de 500 caracteres.")]
        public string Email { get; set; }

        [MaxLength(500, ErrorMessage = "El campo Direccion no puede tener mas de 500 caracteres.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "El campo localizacion es obligatorio.")]
        public int LocalizationCultureId { get; set; }

    }
}
