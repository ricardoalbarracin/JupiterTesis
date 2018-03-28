﻿using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.SEG
{
    public interface IPermisoService
    {
        Result ListPermisos();

        Result ListPermisosUsuario(int usuarioId);

        Result ListPermisosAsignadosUsuario(int usuarioId);
    }
}
