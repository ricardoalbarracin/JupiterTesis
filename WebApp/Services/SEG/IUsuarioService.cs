using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.SEG
{
    public interface IUsuarioService
    {
        Result UsuarioByUserName(string userName);

        Result UsuarioById(int id);

        Result ListUsuarios();

        Result UsuarioEditById(int id);
    }
}
