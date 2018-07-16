using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.SEG
{
    public interface IRoleDAOService
    {
        Result<List<Role>> GetListRoles();
        Result<List<Role>> GetListRolesUsuario(long usuarioId);
        Result<UsuarioRole> InsUsuarioRole(UsuarioRole role);
        Result<Nothing> DelUsuarioRole(UsuarioRole role);
        Result<Role> InsRole(Role role);

    }
}
