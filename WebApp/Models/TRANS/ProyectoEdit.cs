using System;
using System.Collections.Generic;
using Core.Models.PARAM;

namespace Core.Models.TRANS
{
    public class ProyectoEdit
    {
        public Proyecto Proyecto { get; set; }

        public List<ProyectosRubrosView> Rubros { get; set; }
    }
}
