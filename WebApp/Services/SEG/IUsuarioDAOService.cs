using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.SEG
{
    public interface IUsuarioDAOService
    {
        Result GetUsuarioByUserName(string userName);

        Result GetUsuarioById(int id);

        Result GetListUsuarios();

        Result GetUsuarioEditById(int id);

        Result UpdUsuario(Usuario usuario);

        Result InsUsuario(Usuario usuario);

        Result UsuarioByPersonaId(int personaId);

        Result UsuarioByUserIdPersonaId(int userId, int personaId);

        Result GetUsuarioByUserIdUserName(int userId, string userName);
    }
}
