using Core.Models.TRANS;
using Core.Models.Utils;
using Core.Services.TRANS;
using Core.Services.Utils;
using DAOs.Utils;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAOs.PARAM
{
    public class ProyectoDAO : BaseDAO, IProyectoDAOService
    {
        public ProyectoDAO(IDapperAdapter dapper) : base(dapper)
        {
        }
        public Result<List<Proyecto>> GetListProyectos()
        {
            var result = new Result<List<Proyecto>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.GetAll<Proyecto>().ToList();

                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando Proyectos.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<Proyecto> GetProyectoById(long id)
        {
            var result = new Result<Proyecto>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.Get<Proyecto>(id);
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando Proyecto.";
                result.Exception = ex;
            }
            return result;
        }

        public Result UpdProyecto(Proyecto Proyecto)
        {
            var result = new Result();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    connection.Update(Proyecto);
                    result.Message = "Proyecto actualizada correctamente.";
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error actualizando Proyecto.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<Proyecto> InsProyecto(Proyecto Proyecto)
        {
            var result = new Result<Proyecto>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    Proyecto.Id = connection.Insert(Proyecto);
                    result.Message = "Proyecto creada correctamente.";
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error creando Proyecto.";
                result.Exception = ex;
            }
            return result;
        }
    }
}
