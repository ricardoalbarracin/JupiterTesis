﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Core.Business.SEG;
using Core.Models.SEG;
using Core.Models.Utils;

namespace Core.Services.SEG
{
    public class SeguridadServiceProvider : ISeguridadService
    {
        IUsuarioService _usuarioService;
        IRoleService _roleService;
        IPermisoService _permisoService;
        public SeguridadServiceProvider(IUsuarioService usuarioService,IRoleService roleService, IPermisoService permisoService)
        {
            _usuarioService = usuarioService;
            _roleService = roleService;
            _permisoService = permisoService;
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

        public Result UpdUsuarioRolesPermisos(IDictionary<string, object> dataSections)
        {
            using (var transaction = new TransactionScope())
            {
                var usuario = dataSections["UpdUsuario"] as Usuario;
                var updUsuario = _usuarioService.UpdUsuario(usuario);
                if(!updUsuario.Success)
                {
                    return updUsuario;
                }

                var listRoles = dataSections["UpdRolesUsuario"] as Roles;
                foreach (var role in listRoles.ListRoles)
                {
                    if (role.Activo == 1)
                    {
                        var insUsuarioRole = _roleService.InsUsuarioRole(new UsuarioRole(usuario.Id, role.Id));
                        if (!insUsuarioRole.Success)
                        {
                            return insUsuarioRole;
                        }
                    }
                    else
                    {
                        var delUsuarioRole = _roleService.DelUsuarioRole(new UsuarioRole(usuario.Id, role.Id));
                        if (!delUsuarioRole.Success)
                        {
                            return delUsuarioRole;
                        }
                    }
                }

                var ListPermisos = dataSections["UpdPermisosUsuario"] as Permisos;
                foreach (var permiso in ListPermisos.ListPermisos)
                {
                    if (permiso.Activo == 1)
                    {
                        var insUsuarioPermiso = _permisoService.InsUsuarioPermiso(new UsuarioPermiso(usuario.Id, permiso.Id));
                        if (!insUsuarioPermiso.Success)
                        {
                            return insUsuarioPermiso;
                        }
                    }
                    else
                    {
                        var delUsuarioPermiso = _permisoService.DelUsuarioPermiso(new UsuarioPermiso(usuario.Id, permiso.Id));
                        if (!delUsuarioPermiso.Success)
                        {
                            return delUsuarioPermiso;
                        }
                    }
                }

                transaction.Complete();
            }
            return new Result() { Success = true, Message = "se ha actualizado correctacmente el usuario." };
        }

        public Result ResetPassword(Usuario usuario)
        {
            var loginBusiness = new LoginBusiness();

            var generarHashRandomPassword = loginBusiness.GenerarHashRandomPassword(usuario.Username);
            if(!generarHashRandomPassword.Success)
            {
                return generarHashRandomPassword;
            }

            var usuarioById = _usuarioService.UsuarioByUserName(usuario.Username);
            if (!usuarioById.Success)
            {
                return usuarioById;
            }
            var usuarioUpd = usuarioById.Data as Usuario;
            usuarioUpd.Password = generarHashRandomPassword.Data["Hash"];
            var updUsuario = _usuarioService.UpdUsuario(usuarioUpd);
            if (!updUsuario.Success)
            {
                return updUsuario;
            }
            updUsuario.Data = generarHashRandomPassword.Data["Password"];
            return updUsuario;
        }
    }
}