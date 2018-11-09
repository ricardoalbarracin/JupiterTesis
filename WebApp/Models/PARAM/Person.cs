using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models.PARAM
{
    [Table("param.person")]
    public class Person
    {
        [Required(ErrorMessage = "REQUIRED_ERROR_ID")]
        [Display(Name = "PROPERTY_NAME_ID")]
        public int Id { get; set; }


        [Required(ErrorMessage = "REQUIRED_ERROR_DOCUMENTTYPEID")]
        [Display(Name = "PROPERTY_NAME_DOCUMENTTYPEID")]
        public int DocumentTypeId { get; set; }


        [MaxLength(50, ErrorMessage = "LENGTH_ERROR_DOCUMENT")]
        [Required(ErrorMessage = "REQUIRED_ERROR_DOCUMENT")]
        [Display(Name = "PROPERTY_NAME_DOCUMENT")]
        public string Document { get; set; }


        [Display(Name = "PROPERTY_NAME_EXPEDITIONDATE")]
        public DateTime ExpeditionDate { get; set; }


        [MaxLength(1000, ErrorMessage = "LENGTH_ERROR_FIRTSNAME")]
        [Required(ErrorMessage = "REQUIRED_ERROR_FIRTSNAME")]
        [Display(Name = "PROPERTY_NAME_FIRTSNAME")]
        public string FirtsName { get; set; }


        [MaxLength(1000, ErrorMessage = "LENGTH_ERROR_SECONDNAME")]
        [Display(Name = "PROPERTY_NAME_SECONDNAME")]
        public string SecondName { get; set; }


        [MaxLength(1000, ErrorMessage = "LENGTH_ERROR_SURNAME")]
        [Required(ErrorMessage = "REQUIRED_ERROR_SURNAME")]
        [Display(Name = "PROPERTY_NAME_SURNAME")]
        public string Surname { get; set; }


        [MaxLength(1000, ErrorMessage = "LENGTH_ERROR_SECONDSURNAME")]
        [Display(Name = "PROPERTY_NAME_SECONDSURNAME")]
        public string SecondSurname { get; set; }


        [Required(ErrorMessage = "REQUIRED_ERROR_BIRTHDATE")]
        [Display(Name = "PROPERTY_NAME_BIRTHDATE")]
        public DateTime BirthDate { get; set; }


        [Required(ErrorMessage = "REQUIRED_ERROR_GENDERID")]
        [Display(Name = "PROPERTY_NAME_GENDERID")]
        public int GenderId { get; set; }


        [Display(Name = "PROPERTY_NAME_PLACEBIRTHID")]
        public int? PlaceBirthId { get; set; }


        [Display(Name = "PROPERTY_NAME_PLACERESIDENCEID")]
        public int? PlaceResidenceId { get; set; }


        [MaxLength(50, ErrorMessage = "LENGTH_ERROR_PHONE")]
        [Display(Name = "PROPERTY_NAME_PHONE")]
        public string Phone { get; set; }


        [MaxLength(50, ErrorMessage = "LENGTH_ERROR_MOBILE")]
        [Display(Name = "PROPERTY_NAME_MOBILE")]
        public string Mobile { get; set; }


        [MaxLength(500, ErrorMessage = "LENGTH_ERROR_EMAIL")]
        [Required(ErrorMessage = "REQUIRED_ERROR_EMAIL")]
        [Display(Name = "PROPERTY_NAME_EMAIL")]
        public string Email { get; set; }


        [MaxLength(500, ErrorMessage = "LENGTH_ERROR_ADDRESS")]
        [Display(Name = "PROPERTY_NAME_ADDRESS")]
        public string Address { get; set; }


        [Display(Name = "PROPERTY_NAME_LOCALIZATIONCULTUREID")]
        public int LocalizationCultureId { get; set; }


    }
}