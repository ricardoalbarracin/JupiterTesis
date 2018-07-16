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
    public class PermisoDAO : BaseDAO, IPermisoDAOService
    {
        public PermisoDAO(IDapperAdapter dapper) : base(dapper)
        {
        }
        public Result<IEnumerable<Permiso>> GetListPermisos()
        {
            var result = new Result<IEnumerable<Permiso>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.GetAll<Permiso>();
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
        public Result<List<Permiso>> GetListPermisosAsignadosUsuario(long usuarioId)
        {
            var result = new Result<List<Permiso>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    var sql = @"SELECT  p.Id, p.Sigla, p.Descripcion, p.Nombre
                            FROM SEG.Permisos p
                                INNER JOIN SEG.UsuariosPermisos up ON(p.Id = up.PermisoId)
                                    INNER JOIN SEG.Usuarios u ON(up.UsuarioId = u.Id)
                            WHERE u.Id = @Id;";
                    var permisos = connection.Query<Permiso>(sql, new { Id = usuarioId }).ToList();
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
        public Result<List<Permiso>> GetListPermisosUsuario(long usuarioId)
        {
            var result = new Result<List<Permiso>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    var sql = @"SELECT p.Id, p.Sigla, p.Descripcion, p.Nombre
                            FROM SEG.Permisos p
                                INNER JOIN SEG.RolesPermisos rp ON(p.Id = rp.PermisoId)
                                    INNER JOIN SEG.Roles r ON(rp.RoleId = r.Id)
                                        INNER JOIN SEG.UsuariosRoles ur ON(r.Id = ur.RoleId)
                                            INNER JOIN SEG.Usuarios u ON(ur.UsuarioId = u.Id)
                            WHERE u.Id = @Id
                            union
                            SELECT  p.Id, p.Sigla, p.Descripcion, p.Nombre
                            FROM SEG.Permisos p
                                INNER JOIN SEG.UsuariosPermisos up ON(p.Id = up.PermisoId)
                                    INNER JOIN SEG.Usuarios u ON(up.UsuarioId = u.Id)
                            WHERE u.Id = @Id;";
                    var permisos = connection.Query<Permiso>(sql, new { Id = usuarioId }).ToList();
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

        public Result<UsuarioPermiso> InsUsuarioPermiso(UsuarioPermiso permiso)
        {
            var result = new Result<UsuarioPermiso>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    permiso.Id= connection.Insert(permiso);
                    result.Data = permiso;
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
        public Result<Nothing> DelUsuarioPermiso(UsuarioPermiso permiso)
        {
            var result = new Result<Nothing>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    connection.Execute("delete from SEG.UsuariosPermisos where PermisoId = @PermisoId and UsuarioId = @UsuarioId", permiso);
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
