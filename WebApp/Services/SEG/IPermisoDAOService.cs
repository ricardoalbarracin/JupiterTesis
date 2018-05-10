using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.SEG
{
    public interface IPermisoDAOService
    {

        Result<IEnumerable<Permiso>> ListPermisos();
        Result<List<Permiso>> ListPermisosAsignadosUsuario(long usuarioId);

        Result<List<Permiso>> ListPermisosUsuario(long usuarioId);

        Result<UsuarioPermiso> InsUsuarioPermiso(UsuarioPermiso permiso);

        Result<Nothing> DelUsuarioPermiso(UsuarioPermiso permiso);
        

    }
}
