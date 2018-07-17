using Core.Models.PARAM;
using Core.Models.Utils;
using Core.Services.PARAM;
using Core.Services.Utils;
using DAOs.Utils;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAOs.PARAM
{
    public class DependenciaDAO : BaseDAO, IDependenciaDAOService
    {
        public DependenciaDAO(IDapperAdapter dapper) : base(dapper)
        {
        }

        

        public Result<List<Dependencia>> GetListDependencias()
        {
            var result = new Result<List<Dependencia>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.GetAll<Dependencia>().ToList();
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando Dependencias.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<Dependencia> GetDependenciaById(int id)
        {
            var result = new Result<Dependencia>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.Get<Dependencia>(id);
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando Dependencia.";
                result.Exception = ex;
            }
            return result;
        }

        public Result UpdDependencia(Dependencia dependencia)
        {
            var result = new Result();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    connection.Update(dependencia);
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error actualizando Dependencia.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<Dependencia> InsDependencia(Dependencia dependencia)
        {
            var result = new Result<Dependencia>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    dependencia.Id= connection.Insert(dependencia);
                    result.Data = dependencia;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error creando Dependencia.";
                result.Exception = ex;
            }
            return result;
        }

        public Result DelDependencia(int id)
        {
            var result = new Result();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    connection.Execute("delete from CORE.Dependencias where Id = @Id or Padre_id = @Id", new { Id = id });
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error eliminando Dependencias.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<DetalleDependencia> GetDetalleDependenciaById(int id)
        {
            var result = new Result<DetalleDependencia>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.QueryFirst<DetalleDependencia>(@"select d.Codigo,d.Descripcion, p.PrimerNombre + ' ' + p.SegundoNombre + ' ' + p.PrimerApellido + p.SegundoApellido as Colaborador from CORE.Dependencias d
                                                                              inner join ADMIN.Personas p on(d.ColaboradorLiderId = p.Id ) where d.Id = @Id", new { Id= id});
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando Dependencia.";
                result.Exception = ex;
            }
            return result;
        }
    }
}
