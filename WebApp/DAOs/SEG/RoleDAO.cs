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
        public Result<List<Role>> GetListRolesUsuario(long usuarioId)
        {
            var result = new Result<List<Role>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    var sql = @"SELECT r.Id, r.Sigla, r.Nombre, r.Descripcion
                            FROM SEG.Roles r
                                INNER JOIN SEG.UsuariosRoles ur ON(r.Id = ur.RoleId)
                                    INNER JOIN SEG.Usuarios u ON(ur.UsuarioId = u.Id)
                            WHERE u.Id = @Id";
                    var roles = connection.Query<Role>(sql, new { Id = usuarioId }).ToList();
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
        public Result<UsuarioRole> InsUsuarioRole(UsuarioRole role)
        {
            var result = new Result<UsuarioRole>();
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
        public Result DelUsuarioRole(UsuarioRole role)
        {
            var result = new Result();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    connection.Execute("delete from SEG.UsuariosRoles where RoleId = @RoleId and UsuarioId = @UsuarioId", role);
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
