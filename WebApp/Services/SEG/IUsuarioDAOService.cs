using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.SEG
{
    public interface IUsuarioDAOService
    {
        Result<List<UsuarioIdentity>> GetListUsuarios();

        Result<UsuarioIdentity> GetUsuarioById(long id);

        Result<Usuario> GetUsuarioByUserName(string userName);

        Result<UsuarioEdit> GetUsuarioEditById(long id);

        Result<bool> UpdUsuario(Usuario usuario);

        Result<Usuario> InsUsuario(Usuario usuario);

        Result<Usuario> UsuarioByPersonaId(long personaId);

        Result<Usuario> UsuarioByUserIdPersonaId(long userId, long personaId);

        Result<Usuario> GetUsuarioByUserIdUserName(long userId, string userName);
        

    }
}
