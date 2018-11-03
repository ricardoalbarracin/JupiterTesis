using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models.TRANS
{
    public class ComisionColaborador
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "El campo Solicitante es obligatorio.")]
        public long PersonaId { get; set; }

        [Required(ErrorMessage = "El campo Fecha inicio es obligatorio.")]
        public DateTime FechaInicio { get; set; }

        [Required(ErrorMessage = "El campo Número de días es obligatorio.")]
        public int CantidadDias { get; set; }

        [Required(ErrorMessage = "El campo Fecha final es obligatorio.")]
        public DateTime FechaFinalizacion { get; set; }

        [Required(ErrorMessage = "El campo Valor de comisión es obligatorio.")]
        public float ValorComision { get; set; }

        [Required(ErrorMessage = "El campo Destino es obligatorio.")]
        public long Destino { get; set; }

        [Required(ErrorMessage = "El campo Origen de comisión es obligatorio.")]
        public long Origen { get; set; }

        [Required(ErrorMessage = "El campo Justificacion de comisión es obligatorio.")]
        public string Justificacion { get; set; }

        [Display]
        public string EstadoLegalizacion { get; set; }

        [Display]
        public float Estado { get; set; }

        [Required(ErrorMessage = "El campo Colaborador es obligatorio.")]
        public long  ColaboradorId{ get; set; }

        [Required(ErrorMessage = "El campo fecha legalizacion obligatorio.")]
        public DateTime FechaSolicitud { get; set; }

        [Required(ErrorMessage = "El campo proyecto es obligatorio")]
        public long ProyectoId { get; set; }

        [Required(ErrorMessage = "El campo consecutivo es obligatorio")]
        public int Consecutivo { get; set; }

    }
}
