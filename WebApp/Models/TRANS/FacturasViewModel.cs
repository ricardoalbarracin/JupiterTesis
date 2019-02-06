using Core.Models.TRANS;
using Core.Models.Utils;
using Dapper.Contrib.Extensions;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Models.PARAM;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Core.Models.TRANS
{    
    public class FacturasViewModel
    {
        public long Id { get; set; }
        public long ComisionId { get; set; }

        [DisplayName("Valor comisiòn")]
        public float ValorComision { get; set; }

        public List<FacturaIndividualViewModel> ListFacturas { get; set; }

        [DisplayName("Saldo pendiente")]
        public float SubTotal { get; set; }

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
        [DisplayName("Valor factura")]
        public float ValorFactura { get; set; }
    }

    [Table("CORE.FacturasLegalizacion")]
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
        [Write(false)]
        public string ConceptoDescripcion { get; set; }
        public long ConceptoId { get; set; }

        [Required(ErrorMessage = "El campo valor es obligatorio")]
        [DisplayName("Valor")]
        public float ValorFactura { get; set; }

        public float LegalizacionId { get; set; }
        public bool DeleteBand { get; set; }
    }

    [Table("CORE.Legalizaciones")]
    public class Legalizaciones
    {
        public long Id { get; set; }
        public long ComisionId { get; set; }
        public DateTime FechaLegalizacion { get; set; }        
        public int CantidadFacturas { get; set; }        
        public string Estado { get; set; }
        public float ValorFacturas { get; set; }
        public long ColaboradorVerificador { get; set; }
        [Write(false)]
        public List<FacturaIndividualViewModel> ListFacturas { get; set; }

    }
}
