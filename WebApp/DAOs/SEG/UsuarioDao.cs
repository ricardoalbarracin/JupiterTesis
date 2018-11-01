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
    public class UsuarioDao : BaseDAO, IUserDAOService
    {
        IRoleDAOService _roleService;
        IPermissionDAOService _permisoService;
        public UsuarioDao(IDapperAdapter dapper,IRoleDAOService roleService, IPermissionDAOService permisoService):base(dapper)
        {
            _roleService = roleService;
            _permisoService = permisoService;
        }

        public Result<List<UserIdentity>> GetListUser()
        {
            var result = new Result<List<UserIdentity>> ();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.Query<UserIdentity>(@"SELECT u.Id,
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

        public Result<UserIdentity> GetUserById(long id)
        {
            var result = new Result<UserIdentity>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    // Obtiene informacion basica del usuario
                     var sql = @"SELECT u.id,
                                   p.id as Person_Id,
                                   p.firts_Name, 
                                   p.second_name, 
                                   p.birth_date, 
                                   p.surname, 
                                   p.second_Surname,
                                   p.email,
                                   u.username, 
                                   u.password
                            FROM admin.person p 
	                            INNER JOIN seg.user u ON (p.Id = u.person_id)  
                            WHERE u.id = @Id";
                    var usuario = connection.QueryFirst<UserIdentity>(sql, new { Id = id });

                    // Obtiene lista de permisos asociados al usuario
                    var listPermisosUsuario = _permisoService.GetListUserPermissions(id);
                    if (!listPermisosUsuario.Success)
                    {
                        result.Message = listPermisosUsuario.Message;
                        return result;
                    }
                    usuario.Permissions = listPermisosUsuario.Data;

                    // Obtiene lista de roles asociados al usuario
                    var listRolesUsuario = _roleService.GetListUserRoles(id);
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

        public Result<User> GetUserByUserName(string userName)
        {
            var result = new Result<User>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    var sql = @"SELECT id
                                  ,username
                                  ,password
                                  ,person_id
                                  ,active
                              FROM seg.User u
                            WHERE u.Username = @Username;";
                    result.Data = connection.QueryFirst<User>(sql, new { Username = userName });
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

        public Result<UserEdit> GetUserEditById(long id)
        {
            var result = new Result<UserEdit>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    // Obtiene informacion basica del usuario
                    var usuarioEdit = new UserEdit();
                    usuarioEdit.User = connection.Get<User>(id );

                    // Obtiene lista de permisos asociados al usuario
                    var listPermisosUsuario = _permisoService.GetListUserAssignedPermissions(id);
                    if (!listPermisosUsuario.Success)
                    {
                        result.Message = listPermisosUsuario.Message;
                        return result;
                    }
                    usuarioEdit.Permissions = listPermisosUsuario.Data;

                    // Obtiene lista de roles asociados al usuario
                    var listRolesUsuario = _roleService.GetListUserRoles(id);
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

        public Result<bool> UpdUser(User usuario)
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

        public Result<User> InsUser(User usuario)
        {
            var result = new Result<User>();
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
        public Result<User> UserByPersonId(long personaId)
        {
            var result = new Result<User>();
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
                    result.Data = connection.QueryFirst<User>(sql, new { PersonaId = personaId });
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

        public Result<User> UserByUserIdPersonId(long userId, long personaId)
        {
            var result = new Result<User>();
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
                    result.Data = connection.QueryFirst<User>(sql, new { PersonaId = personaId, UserId = userId });
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

        public Result<User> GetUserByUserIdUserName(long userId, string userName)
        {
            var result = new Result<User>();
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
                    result.Data = connection.QueryFirst<User>(sql, new { Username = userName, UserId= userId });
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
