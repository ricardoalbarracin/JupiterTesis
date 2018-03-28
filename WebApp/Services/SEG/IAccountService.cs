using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.SEG
{
    public interface ISeguridadService
    {
        Result Login(Usuario usuario);
    }
}
