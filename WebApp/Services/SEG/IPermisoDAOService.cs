using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.SEG
{
    public interface IPermisoDAOService
    {

        Result<IEnumerable<Permiso>> GetListPermisos();
        Result<List<Permiso>> GetListPermisosAsignadosUsuario(long usuarioId);

        Result<List<Permiso>> GetListPermisosUsuario(long usuarioId);

        Result<UsuarioPermiso> InsUsuarioPermiso(UsuarioPermiso permiso);

        Result<Nothing> DelUsuarioPermiso(UsuarioPermiso permiso);
        

    }
}
