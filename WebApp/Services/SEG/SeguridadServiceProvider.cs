using System;
using System.Collections.Generic;
using System.Text;
using Core.Business.SEG;
using Core.Models.SEG;
using Core.Models.Utils;

namespace Core.Services.SEG
{
    public class SeguridadServiceProvider : ISeguridadService
    {
        IUsuarioService _usuarioService;
        public SeguridadServiceProvider(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        public Result Login(Usuario usuario)
        {
            var getUsuarioByUserName = _usuarioService.UsuarioByUserName(usuario.Username);
            if (!getUsuarioByUserName.Success)
            {
                return  getUsuarioByUserName;
            }

            var usuarioDB = getUsuarioByUserName.Data as Usuario;
            var loginBusiness = new LoginBusiness();
            var validarPassword = loginBusiness.ValidarPassword(usuarioDB,usuario.Password);
            if (!validarPassword.Success)
            {
                return  validarPassword;
            }

            var getUsuarioById = _usuarioService.UsuarioById(usuarioDB.Id);
            if (!getUsuarioById.Success)
            {
                return getUsuarioById;
            }

            return getUsuarioById;
        }
    }
}
