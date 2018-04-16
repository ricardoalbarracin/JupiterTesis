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
        Result UpdUsuarioRolesPermisos(IDictionary<string, object> dataSections);
        Result ResetPassword(Usuario usuario);

        Result InsUsuarioRolesPermisos(IDictionary<string, object> dataSections);

    }
}
