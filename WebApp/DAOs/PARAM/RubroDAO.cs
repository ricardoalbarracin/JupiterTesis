using Core.Models.PARAM;
using Core.Models.Utils;
using Core.Services.PARAM;
using Core.Services.Utils;
using DAOs.Utils;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAOs.PARAM
{
    public class RubroDAO : BaseDAO, IRubroDAOService
    {
        public RubroDAO(IDapperAdapter dapper) : base(dapper)
        {
        }
        public Result<List<Rubro>> GetListRubros()
        {
            var result = new Result<List<Rubro>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.GetAll<Rubro>().ToList();

                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando Rubros.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<Rubro> GetRubroById(long id)
        {
            var result = new Result<Rubro>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.Get<Rubro>(id);
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando Rubro.";
                result.Exception = ex;
            }
            return result;
        }

        public Result UpdRubro(Rubro Rubro)
        {
            var result = new Result();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    connection.Update(Rubro);
                    result.Message = "Rubro actualizada correctamente.";
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error actualizando Rubro.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<Rubro> InsRubro(Rubro Rubro)
        {
            var result = new Result<Rubro>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    Rubro.Id = connection.Insert(Rubro);
                    result.Message = "Rubro creada correctamente.";
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error creando Rubro.";
                result.Exception = ex;
            }
            return result;
        }
    }
}
