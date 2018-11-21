using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using System.Text;

namespace Core.Models.TRANS
{
    [Table("CORE.ColaboradorComision")]
    public class ComisionColaborador
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "El campo Solicitante es obligatorio.")]
        [DisplayName("Solicitante")]
        public long? PersonaId { get; set; }

        [DisplayName("Solicitante")]
        [Write(false)]
        public string NombreSolicitante { get; set; }

        [Required(ErrorMessage = "El campo Fecha inicio es obligatorio.")]
        [DisplayName("Fecha de inicio")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "El campo Número de días es obligatorio.")]
        [DisplayName("Cantidad de días")]
        public int CantidadDias { get; set; }

        [Required(ErrorMessage = "El campo Fecha final es obligatorio.")]
        [DisplayName("Fecha de finalización")]
        public DateTime FechaFinalizacion { get; set; }

        [Required(ErrorMessage = "El campo Valor de comisión es obligatorio.")]
        [DisplayName("Valor comisión")]
        public float ValorComision { get; set; }
        
        [Required(ErrorMessage = "El campo Destino es obligatorio.")]
        [DisplayName("Lugar de destino")]
        public long Destino { get; set; }

        [Required(ErrorMessage = "El campo Origen de comisión es obligatorio.")]
        [DisplayName("Lugar de origen")]
        public long Origen { get; set; }

        [Required(ErrorMessage = "El campo Justificación de comisión es obligatorio.")]
        [DisplayName("Justificación")]
        public string Justificacion { get; set; }

        [DisplayName("Estado de legalización")]
        public string EstadoLegalizacion { get; set; }
                
        public string Estado { get; set; }

        [Required(ErrorMessage = "El campo Colaborador es obligatorio.")]
        [DisplayName("Colaborador")]
        public long  ColaboradorId{ get; set; }

        [Write(false)]
        public string NombreColaborador { get; set; }

        [DisplayName("Fecha de solicitud")]
        public DateTime FechaSolicitud { get; set; }

        [Required(ErrorMessage = "El campo proyecto es obligatorio")]
        [DisplayName("Proyecto")]
        public long ProyectoId { get; set; }

        [Required(ErrorMessage = "El campo consecutivo es obligatorio")]        
        public int Consecutivo { get; set; }

        [DisplayName("Departamento")]
        [Write(false)]
        public int DepartamentoOrigen { get; set; }

        [DisplayName("Departamento")]
        [Write(false)]
        public int DepartamentoDestino { get; set; }
    }

    public class ListGeneral
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public float Valor { get; set; }
    }
}
