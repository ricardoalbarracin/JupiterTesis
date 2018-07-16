using AutoMapper;
using Core.Models.SEG;
using Core.Models.Utils;
using Core.Services.SEG;
using Core.Services.Utils;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Core.Business.SEG
{
    public class SeguridadServiceBusiness : ISeguridadService
    {
        IUsuarioDAOService _usuarioService;
        IRoleDAOService _roleService;
        IPermisoDAOService _permisoService;
        IEmailSender _emailSender;
        public SeguridadServiceBusiness(IUsuarioDAOService usuarioService,
                                        IRoleDAOService roleService,
                                        IPermisoDAOService permisoService,
                                        IEmailSender emailSender)
        {
            _usuarioService = usuarioService;
            _roleService = roleService;
            _permisoService = permisoService;
            _emailSender = emailSender;
        }

        public Result<UsuarioIdentity> Login(Usuario usuario)
        {
            var result = new Result<UsuarioIdentity>();
            var getUsuarioByUserName = _usuarioService.GetUsuarioByUserName(usuario.Username);
            if (!getUsuarioByUserName.Success)
            {
                result.Message = getUsuarioByUserName.Message;
                return result;
            }

            var usuarioDB = getUsuarioByUserName.Data as Usuario;
           
            var validarPassword = ValidarPassword(usuarioDB,usuario.Password);
            if (!validarPassword.Success)
            {
                result.Message = validarPassword.Message;
                return result;
            }

            result = _usuarioService.GetUsuarioById(usuarioDB.Id);
            if (!result.Success)
            {
                return result;
            }

            return result;
        }

        public Result UpdUsuarioRolesPermisos(IDictionary<string, object> dataSections)
        {
            var result = new Result();
            using (var transaction = new TransactionScope())
            {
                var usuario = dataSections["UpdUsuario"] as Usuario;
                var usuarioById = _usuarioService.GetUsuarioById(usuario.Id) ;
                if (!usuarioById.Success)
                {
                    result.Message = usuarioById.Message;
                    return result;
                }
                var usuarioDb = usuarioById.Data as UsuarioIdentity;

                usuario.Password = usuarioDb.Password;
                var updUsuario = _usuarioService.UpdUsuario(usuario);

                if (!updUsuario.Success)
                {
                    result.Message = updUsuario.Message;
                    return result;
                }

                var listRoles = dataSections["UpdRolesUsuario"] as Roles;
                foreach (var role in listRoles.ListRoles)
                {
                    if (role.Activo == 1)
                    {
                        var insUsuarioRole = _roleService.InsUsuarioRole(new UsuarioRole(usuario.Id, role.Id));
                        if (!insUsuarioRole.Success)
                        {
                            result.Message = insUsuarioRole.Message;
                            return result;
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
                            result.Message = insUsuarioPermiso.Message;
                            return result;
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
            result.Success = true;
            result.Message  = "se ha actualizado correctacmente el usuario.";
            return result;
        }

        public Result InsUsuarioRolesPermisos(IDictionary<string, object> dataSections)
        {
            var result = new Result();
            using (var transaction = new TransactionScope())
            {
                var usuario = dataSections["InsUsuario"] as Usuario;

               
                var generarHashRandomPassword = GenerarHashRandomPassword(usuario.Username);
                if (!generarHashRandomPassword.Success)
                {
                    result.Message = generarHashRandomPassword.Message;
                    return result;
                }


                usuario.Password = generarHashRandomPassword.Data["Hash"];
                var insUsuario = _usuarioService.InsUsuario(usuario);

                if (!insUsuario.Success)
                {
                    result.Message = insUsuario.Message;
                    return result;
                }
                usuario = insUsuario.Data as Usuario;
                var listRoles = dataSections["InsRolesUsuario"] as Roles;
                foreach (var role in listRoles.ListRoles)
                {
                    if (role.Activo == 1)
                    {
                        var insUsuarioRole = _roleService.InsUsuarioRole(new UsuarioRole(usuario.Id, role.Id));
                        if (!insUsuarioRole.Success)
                        {
                            result.Message = insUsuarioRole.Message;
                            return result;
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
                            result.Message = insUsuarioPermiso.Message;
                            return result;
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
            result.Success = true;
            result.Message = "se ha creado correctacmente el usuario.";
            return result;
           
        }

        public Result<string> ResetPassword(Usuario usuario)
        {
            var result = new Result<string>();

            var generarHashRandomPassword = GenerarHashRandomPassword(usuario.Username);
            if(!generarHashRandomPassword.Success)
            {
                result.Message = generarHashRandomPassword.Message;
                return result;
            }

            var usuarioById = _usuarioService.GetUsuarioById(usuario.Id);
            if (!usuarioById.Success)
            {
                result.Message = usuarioById.Message;
                return result;
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
                result.Message = updUsuario.Message;
                return result;
            }

            var sendEmailResetPassword = _emailSender.SendEmailResetPassword(usuarioIdentity.Correo, usuarioUpd.Username,usuarioUpd.Password);

            if (!sendEmailResetPassword.Success)
            {
                result.Success = true;
                result.Data = "warning";
                result.Message = $"Se ha generado y actualizado la contraseña a : <b>{generarHashRandomPassword.Data["Password"]}</b>.</br> Se ha generado un error al intentar enviar correo electronico.";
                return result;
            }
            result.Data = "success";
            result.Message = $"Se ha generado y actualizado la contraseña a : <b>{generarHashRandomPassword.Data["Password"]}</b>.";
            result.Success = true;
            return result;
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

        public Result<Dictionary<string, string>> GenerarHashRandomPassword(string Username)
        {
            var result = new Result<Dictionary<string, string>>();
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

        public Result ValidarCrearUsuario(Usuario usuario)
        {
            var result =new  Result();
            var usuarioByPersonaId = _usuarioService.UsuarioByPersonaId(usuario.PersonaId);

            if(usuarioByPersonaId.Success)
            {
                result.Success = false;
                result.Message = "La persona ya tiene un usuario asociado";
                return result;
            }

            var getUsuarioByUserName = _usuarioService.GetUsuarioByUserName(usuario.Username);

            if (getUsuarioByUserName.Success)
            {
                result.Success = false;
                result.Message = "El nombre de usuario ya existe";
                return result;
            }
            result.Message = "Usuario validado correctamente";
            result.Success= true;
            return result;
        }

        public Result ValidarActualizarUsuario(Usuario usuario)
        {
            var result = new Result();
            var usuarioByUserIdPersonaId = _usuarioService.UsuarioByUserIdPersonaId(usuario.Id, usuario.PersonaId);

            if (usuarioByUserIdPersonaId.Success)
            {
                result.Success = false;
                result.Message = "La persona ya tiene un usuario asociado";
                return result;
            }

            var getUsuarioByUserIdUserName = _usuarioService.GetUsuarioByUserIdUserName(usuario.Id, usuario.Username);

            if (getUsuarioByUserIdUserName.Success)
            {
                result.Success = false;
                result.Message = "El nombre de usuario ya existe";
                return result;
            }
            result.Message = "Usuario validado correctamente";
            result.Success = true;
            return result;
        }
    }
}
