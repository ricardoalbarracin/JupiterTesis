using System;
using System.Collections.Generic;
using System.Text;
using Core.Business.SEG;
using Core.Models.SEG;
using Core.Models.Utils;

namespace Core.Services.SEG
{
    public class AccountServiceProvider : IAccountService
    {
        IUsuarioService _usuarioService;
        public AccountServiceProvider(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        public Result Login(Usuario usuario)
        {
            var getUsuarioByUserName = _usuarioService.GetUsuarioByUserName(usuario.Username);
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

            var getUsuarioById = _usuarioService.GetUsuarioById(usuarioDB.Id);
            if (!getUsuarioById.Success)
            {
                return getUsuarioById;
            }

            return getUsuarioById;
        }
    }
}
