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
    public class PermissionDAO : BaseDAO, IPermissionDAOService
    {
        public PermissionDAO(IDapperAdapter dapper) : base(dapper)
        {
        }
        public Result<IEnumerable<Permission>> GetListPermissions()
        {
            var result = new Result<IEnumerable<Permission>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.GetAll<Permission>();
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando permisos.";
                result.Exception = ex;
            }
            return result;
        }
        public Result<List<Permission>> GetListUserAssignedPermissions(long userId)
        {
            var result = new Result<List<Permission>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    var sql = @"SELECT  p.id, p.code, p.full_description, p.description
                            FROM seg.permission p
                                INNER JOIN seg.user_permission up ON(p.id = up.Permission_id)
                                    INNER JOIN seg.user u ON(up.user_id = u.id)
                            WHERE u.id = @Id;";
                    var permisos = connection.Query<Permission>(sql, new { Id = userId }).ToList();
                    result.Data = permisos;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando permisos.";
                result.Exception = ex;
            }
            return result;
        }
        public Result<List<Permission>> GetListUserPermissions(long userId)
        {
            var result = new Result<List<Permission>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    var sql = @"SELECT p.id, p.code, p.full_description, p.description
                            FROM seg.permission p
                                INNER JOIN seg.role_Permission rp ON(p.id = rp.permission_id)
                                    INNER JOIN seg.role r ON(rp.role_id = r.id)
                                        INNER JOIN seg.user_role ur ON(r.id = ur.role_id)
                                            INNER JOIN seg.user u ON(ur.user_id = u.id)
                            WHERE u.id = @Id
                            union
                            SELECT  p.id, p.code, p.full_description, p.description
                            FROM seg.permission p
                                INNER JOIN seg.user_permission up ON(p.id = up.Permission_id)
                                    INNER JOIN seg.user u ON(up.user_id = u.id)
                            WHERE u.id = @Id;";
                    var permisos = connection.Query<Permission>(sql, new { Id = userId }).ToList();
                    result.Data = permisos;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando permisos.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<UserPermision> InsUserPermission(UserPermision permission)
        {
            var result = new Result<UserPermision>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    permission.Id= connection.Insert(permission);
                    result.Data = permission;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error insertando permiso a usuario.";
                result.Exception = ex;
            }
            return result;
        }
        public Result DelUserPermission(UserPermision permission)
        {
            var result = new Result();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    connection.Execute("delete from seg.user_permission where Permission_id = @PermisoId and user_id = @UsuarioId", permission);
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error borrando permiso a usuario.";
                result.Exception = ex;
            }
            return result;
        }
    }
}
