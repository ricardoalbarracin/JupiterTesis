using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Models.CORE
{
    public class ColaboradorGrid
    {
        public long Id { get; set; }

        private string _username = "";

        public long? PersonaId { get; set; }

        public string PrimerNombre { get; set; }

        public string SegundoNombre { get; set; }

        public string PrimerApellido { get; set; }

        public string SegundoApellido { get; set; }

        public string Documento { get; set; }

        public string Cargo { get; set; }

    }
}
