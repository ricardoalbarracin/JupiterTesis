using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.SEG
{
    public interface IPermisoDAOService
    {
        Result ListPermisos();

        Result ListPermisosUsuario(int usuarioId);

        Result ListPermisosAsignadosUsuario(int usuarioId);

        Result InsUsuarioPermiso(UsuarioPermiso permiso);

        Result DelUsuarioPermiso(UsuarioPermiso permiso);
    }
}
