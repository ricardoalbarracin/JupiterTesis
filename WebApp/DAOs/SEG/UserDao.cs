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
    public class UserDao : BaseDAO, IUserDAOService
    {
        IRoleDAOService _roleService;
        IPermissionDAOService _permisoService;
        public UserDao(IDapperAdapter dapper,IRoleDAOService roleService, IPermissionDAOService permisoService):base(dapper)
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
                    result.Data = connection.Query<UserIdentity>(@"SELECT u.id,
                                   p.document,
                                   p.firts_name,
                                   p.second_name,
                                   p.birth_date,
                                   p.surname,
                                   p.second_surname,
                                   u.username,
                                   u.password
                            FROM admin.Person p

                                INNER JOIN seg.user u ON(p.id = u.person_id) ").AsList();
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
	                            INNER JOIN seg.user u ON (p.id = u.person_id)  
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

        public Result<bool> UpdUser(User user)
        {
            var result = new Result<bool>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data= connection.Update(user);
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

        public Result<User> InsUser(User user)
        {
            var result = new Result<User>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    user.Id= (int)connection.Insert(user);
                    result.Data = user;
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
        public Result<User> UserByPersonId(long personId)
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
                                  ,Active
                              FROM seg.user u
                            WHERE u.person_Id = @PersonaId;";
                    result.Data = connection.QueryFirst<User>(sql, new { PersonId = personId });
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
                    var sql = @"SELECT Id
                                  ,username
                                  ,password
                                  ,person_id
                                  ,active
                              FROM seg.User u
                            WHERE u.person_id = @PersonaId and id <> @UserId;";
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
                    var sql = @"SELECT Id
                                  ,username
                                  ,password
                                  ,person_id
                                  ,active
                              FROM seg.user u
                            WHERE u.username = @Username and Id <> @UserId;";
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
