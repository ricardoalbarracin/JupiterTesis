using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.SEG
{
    public interface ISeguridadService
    {
        Result<UsuarioIdentity> Login(Usuario usuario);
        Result UpdUsuarioRolesPermisos(IDictionary<string, object> dataSections);
        Result InsUsuarioRolesPermisos(IDictionary<string, object> dataSections);
        Result<string> ResetPassword(Usuario usuario);

        Result ValidarPassword(Usuario usuario, string password);
        Result<Dictionary<string, string>> GenerarHashRandomPassword(string Username);
        string GenerateRandomPassword();
        Result ValidarCrearUsuario(Usuario usuario);
        Result ValidarActualizarUsuario(Usuario usuario);
    }
}
