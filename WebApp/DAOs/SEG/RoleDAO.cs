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
    public class RoleDAO : BaseDAO, IRoleDAOService
    {
        public RoleDAO(IDapperAdapter dapper) : base(dapper)
        {
        }
        public Result<List<Role>> GetListRoles()
        {
            var result = new Result<List<Role>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.GetAll<Role>().ToList();
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando roles.";
                result.Exception = ex;
            }
            return result;
        }
        public Result<List<Role>> GetListUserRoles(long userId)
        {
            var result = new Result<List<Role>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    var sql = @"SELECT r.id, r.code, r.description, r.full_description
                            FROM seg.role r
                                INNER JOIN seg.user_Role ur ON(r.id = ur.role_id)
                                    INNER JOIN seg.user u ON(ur.user_id = u.id)
                            WHERE u.id = @Id";
                    var roles = connection.Query<Role>(sql, new { Id = userId }).ToList();
                    result.Data = roles;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando roles.";
                result.Exception = ex;
            }
            return result;
        }
        public Result<UserRole> InsUserRole(UserRole role)
        {
            var result = new Result<UserRole>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    role.Id = connection.Insert(role);
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error insertando rol a usuario.";
                result.Exception = ex;
            }
            return result;
        }
        public Result DelUserRole(UserRole role)
        {
            var result = new Result();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    connection.Execute("delete from seg.User_Role where role_id = @RoleId and user_id = @UsuarioId", role);
                }
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Message = "Error borrando rol a usuario.";
                result.Exception = ex;
            }
            
            return result;
        }
        public Result<Role> InsRole(Role role)
        {
            var result = new Result<Role>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    role.Id= connection.Insert(role);
                    result.Data = role;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error insertando rol.";
                result.Exception = ex;
            }
            return result;
        }
    }
}
