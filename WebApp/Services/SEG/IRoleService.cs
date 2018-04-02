using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.SEG
{
    public interface IRoleService
    {
        Result ListRoles();

        Result ListRolesUsuario(int usuarioId);
        Result InsRole(Role role);
        Result InsUsuarioRole(UsuarioRole role);
        Result DelUsuarioRole(UsuarioRole role);
    }
}
