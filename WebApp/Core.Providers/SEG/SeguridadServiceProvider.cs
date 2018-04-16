using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;
using AutoMapper;
using Core.Models.SEG;
using Core.Models.Utils;
using Core.Services.SEG;
using Core.Services.Utils;
using Microsoft.AspNetCore.Identity;

namespace Core.Providers.SEG
{
    public class SeguridadServiceProvider : ISeguridadService
    {
        IUsuarioDAOService _usuarioService;
        IRoleDAOService _roleService;
        IPermisoDAOService _permisoService;
        IEmailSender _emailSender;
        public SeguridadServiceProvider(IUsuarioDAOService usuarioService,
                                        IRoleDAOService roleService, 
                                        IPermisoDAOService permisoService,
                                        IEmailSender emailSender)
        {
            _usuarioService = usuarioService;
            _roleService = roleService;
            _permisoService = permisoService;
            _emailSender = emailSender;
        }

        public Result Login(Usuario usuario)
        {
            var getUsuarioByUserName = _usuarioService.UsuarioByUserName(usuario.Username);
            if (!getUsuarioByUserName.Success)
            {
                return  getUsuarioByUserName;
            }

            var usuarioDB = getUsuarioByUserName.Data as Usuario;
           
            var validarPassword = ValidarPassword(usuarioDB,usuario.Password);
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
                var usuarioById = _usuarioService.UsuarioById(usuario.Id) ;
                if (!usuarioById.Success)
                {
                    return usuarioById;
                }
                var usuarioDb = usuarioById.Data as UsuarioIdentity;

                usuario.Password = usuarioDb.Password;
                var updUsuario = _usuarioService.UpdUsuario(usuario);

                if (!updUsuario.Success)
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

        public Result InsUsuarioRolesPermisos(IDictionary<string, object> dataSections)
        {
            using (var transaction = new TransactionScope())
            {
                var usuario = dataSections["InsUsuario"] as Usuario;

               
                var generarHashRandomPassword = GenerarHashRandomPassword(usuario.Username);
                if (!generarHashRandomPassword.Success)
                {
                    return generarHashRandomPassword;
                }


                usuario.Password = generarHashRandomPassword.Data["Hash"];
                var insUsuario = _usuarioService.InsUsuario(usuario);

                if (!insUsuario.Success)
                {
                    return insUsuario;
                }

                var listRoles = dataSections["InsRolesUsuario"] as Roles;
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

                var ListPermisos = dataSections["InsPermisosUsuario"] as Permisos;
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
            return new Result() { Success = true, Message = "se ha creado correctacmente el usuario." };
        }

        public Result ResetPassword(Usuario usuario)
        {
            var generarHashRandomPassword = GenerarHashRandomPassword(usuario.Username);
            if(!generarHashRandomPassword.Success)
            {
                return generarHashRandomPassword;
            }

            var usuarioById = _usuarioService.UsuarioById(usuario.Id);
            if (!usuarioById.Success)
            {
                return usuarioById;
            }
            var usuarioIdentity = usuarioById.Data as UsuarioIdentity;

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<UsuarioIdentity, Usuario>();
            });

            IMapper mapper = config.CreateMapper();

            var usuarioUpd = mapper.Map<UsuarioIdentity, Usuario>(usuarioIdentity); 
            
            usuarioUpd.Password = generarHashRandomPassword.Data["Hash"];
            var updUsuario = _usuarioService.UpdUsuario(usuarioUpd);
            if (!updUsuario.Success)
            {
                return updUsuario;
            }

            var sendEmailResetPassword = _emailSender.SendEmailResetPassword(usuarioIdentity.Correo, usuarioUpd.Username,usuarioUpd.Password);

            if (!sendEmailResetPassword.Success)
            {
                sendEmailResetPassword.Success = true;
                sendEmailResetPassword.Data = "warning";
                sendEmailResetPassword.Message = $"Se ha generado y actualizado la contraseña a : <b>{generarHashRandomPassword.Data["Password"]}</b>.</br> Se ha generado un error al intentar enviar correo electronico.";
                return sendEmailResetPassword;
            }
            sendEmailResetPassword.Data = "success";
            updUsuario.Message = $"Se ha generado y actualizado la contraseña a : <b>{generarHashRandomPassword.Data["Password"]}</b>.";
            return updUsuario;
        }

        public Result ValidarPassword(Usuario usuario, string password)
        {
            var result = new Result();
            PasswordHasher<string> pw = new PasswordHasher<string>();
            var resultVerify = pw.VerifyHashedPassword(usuario.Username, usuario.Password, password);
            if (resultVerify == PasswordVerificationResult.Failed)
            {
                result.Message = "Verifique sus credenciales de acceso previamente";
                return result;
            }
            result.Success = true;
            return result;
        }

        public Result GenerarHashRandomPassword(string Username)
        {
            var result = new Result();
            try
            {
                var password = GenerateRandomPassword();
                PasswordHasher<string> pw = new PasswordHasher<string>();
                var data = new Dictionary<string, string>();
                data.Add("Password", password);
                data.Add("Hash", pw.HashPassword(Username, password));
                result.Data = data;
                result.Success = true;
            }
            catch (System.Exception ex)
            {
                result.Exception = ex;
            }
            return result;
        }

        public string GenerateRandomPassword()
        {
            var opts = new PasswordOptions()
            {
                RequiredLength = 8,
                RequiredUniqueChars = 4,
                RequireDigit = true,
                RequireLowercase = false,
                RequireNonAlphanumeric = false,
                RequireUppercase = true
            };

            string[] randomChars = new[] {
                "ABCDEFGHJKLMNOPQRSTUVWXYZ",    // uppercase 
                "0123456789"                   // digits
                
            };

            Random rand = new Random(Environment.TickCount);
            List<char> chars = new List<char>();

            if (opts.RequireUppercase)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[0][rand.Next(0, randomChars[0].Length)]);
            if (opts.RequireDigit)
                chars.Insert(rand.Next(0, chars.Count),
                    randomChars[1][rand.Next(0, randomChars[1].Length)]);

            for (int i = chars.Count; i < opts.RequiredLength
                || chars.Distinct().Count() < opts.RequiredUniqueChars; i++)
            {
                string rcs = randomChars[rand.Next(0, randomChars.Length)];
                chars.Insert(rand.Next(0, chars.Count),
                    rcs[rand.Next(0, rcs.Length)]);
            }

            return new string(chars.ToArray());
        }


    }
}
