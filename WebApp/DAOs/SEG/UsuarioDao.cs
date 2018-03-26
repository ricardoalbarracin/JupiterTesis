using Core.Models.SEG;
using Core.Models.Utils;
using Core.Services.SEG;
using Core.Services.Utils;
using DAOs.Utils;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAOs.SEG
{
    public class UsuarioDao:BaseDAO, IUsuarioService
    {
        public UsuarioDao(IDapperAdapter dapper):base(dapper)
        {
        }

        public Result GetListUsuarios()
        {
            var result = new Result();
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

                                INNER JOIN SEG.Usuarios u ON(p.Id = u.PersonaId) ");
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

        public Result GetUsuarioById(int id)
        {
            var result = new Result();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    // Obtiene informacion basica del usuario
                     var sql = @"SELECT u.Id,
                                   p.PrimerNombre, 
                                   p.SegundoNombre, 
                                   p.FechaNacimiento, 
                                   p.PrimerApellido, 
                                   p.SegundoApellido,
                                   u.Username, 
                                   u.Password
                            FROM ADMIN.Personas p 
	                            INNER JOIN SEG.Usuarios u ON (p.Id = u.PersonaId)  
                            WHERE u.Id = @Id";
                    var usuario = connection.QueryFirst<UsuarioIdentity>(sql, new { Id = id });

                    // Obtiene lista de permisos asociados al usuario
                    sql = @"SELECT p.Sigla
                            FROM SEG.Permisos p
                                INNER JOIN SEG.RolesPermisos rp ON(p.Id = rp.PermisoId)
                                    INNER JOIN SEG.Roles r ON(rp.RoleId = r.Id)
                                        INNER JOIN SEG.UsuariosRoles ur ON(r.Id = ur.RoleId)
                                            INNER JOIN SEG.Usuarios u ON(ur.UsuarioId = u.Id)
                            WHERE u.Id = @Id
                            union
                            SELECT p.Sigla
                            FROM SEG.Permisos p
                                INNER JOIN SEG.UsuariosPermisos up ON(p.Id = up.PermisoId)
                                    INNER JOIN SEG.Usuarios u ON(up.UsuarioId = u.Id)
                            WHERE u.Id = @Id;";
                    var permisos = connection.Query<string>(sql, new { Id = id }).ToList();
                    usuario.Permisos = permisos;
                    

                    // Obtiene lista de roles asociados al usuario
                    sql = @"SELECT r.Sigla
                            FROM SEG.Roles r
                                INNER JOIN SEG.UsuariosRoles ur ON(r.Id = ur.RoleId)
                                    INNER JOIN SEG.Usuarios u ON(ur.UsuarioId = u.Id)
                            WHERE u.Id = @Id";
                    var roles = connection.Query<string>(sql, new { Id = id}).ToList();
                    usuario.Roles = roles;

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

        public Result GetUsuarioByUserName(string userName)
        {
            var result = new Result();
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
    }
}
