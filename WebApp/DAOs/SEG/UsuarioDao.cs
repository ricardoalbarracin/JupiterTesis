using Core.Models.SEG;
using Core.Models.Utils;
using Core.Services.SEG;
using Core.Services.Utils;
using DAOs.Utils;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DAOs.SEG
{
    public class UsuarioDao : BaseDAO, IUsuarioDAOService
    {
        IRoleDAOService _roleService;
        IPermisoDAOService _permisoService;
        public UsuarioDao(IDapperAdapter dapper,IRoleDAOService roleService, IPermisoDAOService permisoService):base(dapper)
        {
            _roleService = roleService;
            _permisoService = permisoService;
        }

        public Result<List<UsuarioIdentity>> GetListUsuarios()
        {
            var result = new Result<List<UsuarioIdentity>> ();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.Query<UsuarioIdentity>(@"SELECT u.Id,
                                   p.Documento,
                                   p.PrimerNombre,
                                   p.SegundoNombre,
                                   p.FechaNacimiento,
                                   p.PrimerApellido,
                                   p.SegundoApellido,
                                   u.Username,
                                   u.Password
                            FROM ADMIN.Personas p

                                INNER JOIN SEG.Usuarios u ON(p.Id = u.PersonaId) ").AsList();
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando usuarios";
                result.Exception = ex; 
            }

            return result;
        }

        public Result<UsuarioIdentity> GetUsuarioById(long id)
        {
            var result = new Result<UsuarioIdentity>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    // Obtiene informacion basica del usuario
                     var sql = @"SELECT u.Id,
                                   p.Id as PersonaId,
                                   p.PrimerNombre, 
                                   p.SegundoNombre, 
                                   p.FechaNacimiento, 
                                   p.PrimerApellido, 
                                   p.SegundoApellido,
                                   p.Correo,
                                   u.Username, 
                                   u.Password
                            FROM ADMIN.Personas p 
	                            INNER JOIN SEG.Usuarios u ON (p.Id = u.PersonaId)  
                            WHERE u.Id = @Id";
                    var usuario = connection.QueryFirst<UsuarioIdentity>(sql, new { Id = id });

                    // Obtiene lista de permisos asociados al usuario
                    var listPermisosUsuario = _permisoService.GetListPermisosUsuario(id);
                    if (!listPermisosUsuario.Success)
                    {
                        result.Message = listPermisosUsuario.Message;
                        return result;
                    }
                    usuario.Permisos = listPermisosUsuario.Data;

                    // Obtiene lista de roles asociados al usuario
                    var listRolesUsuario = _roleService.GetListRolesUsuario(id);
                    if (!listRolesUsuario.Success)
                    {
                        result.Message = listRolesUsuario.Message;
                        return result;
                    }
                    usuario.Roles = listRolesUsuario.Data;

                    // Actualiza informacion del objeto Usuario.
                    result.Data = usuario;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando usuario";
                result.Exception = ex;
            }

            return result;
        }

        public Result<Usuario> GetUsuarioByUserName(string userName)
        {
            var result = new Result<Usuario>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    var sql = @"SELECT [Id]
                                  ,[Username]
                                  ,[Password]
                                  ,[PersonId]
                                  ,[Active]
                              FROM [seg].[User] u
                            WHERE u.Username = @Username;";
                    result.Data = connection.QueryFirst<Usuario>(sql, new { Username = userName });
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando usuario";
                result.Exception = ex;
                return result;
            }
            result.Success = true;
            return result;
        }

        public Result<UsuarioEdit> GetUsuarioEditById(long id)
        {
            var result = new Result<UsuarioEdit>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    // Obtiene informacion basica del usuario
                    var usuarioEdit = new UsuarioEdit();
                    usuarioEdit.Usuario = connection.Get<Usuario>(id );

                    // Obtiene lista de permisos asociados al usuario
                    var listPermisosUsuario = _permisoService.GetListPermisosAsignadosUsuario(id);
                    if (!listPermisosUsuario.Success)
                    {
                        result.Message = listPermisosUsuario.Message;
                        return result;
                    }
                    usuarioEdit.Permisos = listPermisosUsuario.Data;

                    // Obtiene lista de roles asociados al usuario
                    var listRolesUsuario = _roleService.GetListRolesUsuario(id);
                    if (!listRolesUsuario.Success)
                    {
                        result.Message = listRolesUsuario.Message;
                        return result;
                    }
                    usuarioEdit.Roles = listRolesUsuario.Data;

                    // Actualiza informacion del objeto Usuario.
                    result.Data = usuarioEdit;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando usuario";
                result.Exception = ex;
            }

            return result;
        }

        public Result<bool> UpdUsuario(Usuario usuario)
        {
            var result = new Result<bool>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data= connection.Update(usuario);
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error actualizando usuario";
                result.Exception = ex;
            }
            return result;
        }

        public Result<Usuario> InsUsuario(Usuario usuario)
        {
            var result = new Result<Usuario>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    usuario.Id= (int)connection.Insert(usuario);
                    result.Data = usuario;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error creando usuario";
                result.Exception = ex;
            }
            return result;
        }
        public Result<Usuario> UsuarioByPersonaId(long personaId)
        {
            var result = new Result<Usuario>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    var sql = @"SELECT [Id]
                                  ,[Username]
                                  ,[Password]
                                  ,[PersonaId]
                                  ,[Activo]
                              FROM [SEG].[Usuarios] u
                            WHERE u.PersonaId = @PersonaId;";
                    result.Data = connection.QueryFirst<Usuario>(sql, new { PersonaId = personaId });
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando usuario";
                result.Exception = ex;
                return result;
            }
            result.Success = true;
            return result;
        }

        public Result<Usuario> UsuarioByUserIdPersonaId(long userId, long personaId)
        {
            var result = new Result<Usuario>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    var sql = @"SELECT [Id]
                                  ,[Username]
                                  ,[Password]
                                  ,[PersonaId]
                                  ,[Activo]
                              FROM [SEG].[Usuarios] u
                            WHERE u.PersonaId = @PersonaId and Id <> @UserId;";
                    result.Data = connection.QueryFirst<Usuario>(sql, new { PersonaId = personaId, UserId = userId });
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando usuario";
                result.Exception = ex;
                return result;
            }
            result.Success = true;
            return result;
        }

        public Result<Usuario> GetUsuarioByUserIdUserName(long userId, string userName)
        {
            var result = new Result<Usuario>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    var sql = @"SELECT [Id]
                                  ,[Username]
                                  ,[Password]
                                  ,[PersonaId]
                                  ,[Activo]
                              FROM [SEG].[Usuarios] u
                            WHERE u.Username = @Username and Id <> @UserId;";
                    result.Data = connection.QueryFirst<Usuario>(sql, new { Username = userName, UserId= userId });
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando usuario";
                result.Exception = ex;
                return result;
            }
            result.Success = true;
            return result;
        }
    }
}
