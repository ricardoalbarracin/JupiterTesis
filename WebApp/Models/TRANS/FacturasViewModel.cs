using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models.TRANS
{
    public class FacturasViewModel
    {
        public long ComisionId { get; set; }

        [DisplayName("Valor comisiòn")]
        public long ValorComision { get; set; }

        public List<FacturaIndividualViewModel> ListFacturas { get; set; }

        [DisplayName("Saldo pendiente")]
        public long SubTotal { get; set; }

        [Required(ErrorMessage = "El campo establecimiento es obligatorio")]
        [DisplayName("Establecimiento")]
        public string RazonSocial { get; set; }

        [Required(ErrorMessage = "El campo Fecha es obligatorio")]
        [DisplayName("Fecha")]
        public DateTime FechaFactura { get; set; }

        [Required(ErrorMessage = "El campo Nit es obligatorio")]
        [DisplayName("Nit")]
        public string Nit { get; set; }

        [Required(ErrorMessage = "El campo concepto es obligatorio")]
        [DisplayName("Concepto")]
        public long ConceptoId { get; set; }

        [Required(ErrorMessage = "El campo valor es obligatorio")]
        [DisplayName("Valor")]
        public DateTime ValorFactura { get; set; }
    }

    public class FacturaIndividualViewModel
    {
        public long Id { get; set; }       

        [Required(ErrorMessage = "El campo establecimiento es obligatorio")]
        [DisplayName("Establecimiento")]
        public string RazonSocial { get; set; }

        [Required(ErrorMessage = "El campo Fecha es obligatorio")]
        [DisplayName("Fecha")]
        public DateTime FechaFactura { get; set; }

        [Required(ErrorMessage = "El campo Nit es obligatorio")]
        [DisplayName("Nit")]
        public string Nit { get; set; }

        [Required(ErrorMessage = "El campo concepto es obligatorio")]
        [DisplayName("Concepto")]
        public long ConceptoId { get; set; }

        [Required(ErrorMessage = "El campo valor es obligatorio")]
        [DisplayName("Valor")]
        public DateTime ValorFactura { get; set; }
    }
}
