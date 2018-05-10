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
        Result<Nothing> UpdUsuarioRolesPermisos(IDictionary<string, object> dataSections);
        Result<Nothing> InsUsuarioRolesPermisos(IDictionary<string, object> dataSections);
        Result<string> ResetPassword(Usuario usuario);

        Result<Nothing> ValidarPassword(Usuario usuario, string password);
        Result<Dictionary<string, string>> GenerarHashRandomPassword(string Username);
        string GenerateRandomPassword();
        Result<Nothing> ValidarCrearUsuario(Usuario usuario);
        Result<Nothing> ValidarActualizarUsuario(Usuario usuario);
    }
}
