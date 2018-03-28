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
    }
}
