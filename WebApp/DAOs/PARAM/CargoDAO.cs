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
    public class CargoDAO : BaseDAO, ICargoDAOService
    {
        public CargoDAO(IDapperAdapter dapper) : base(dapper)
        {
        }
        public Result<List<Cargo>> GetListCargos()
        {
            var result = new Result<List<Cargo>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.GetAll<Cargo>().ToList();

                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando Cargos.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<Cargo> GetCargoById(long id)
        {
            var result = new Result<Cargo>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.Get<Cargo>(id);
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando Cargo.";
                result.Exception = ex;
            }
            return result;
        }

        public Result UpdCargo(Cargo Cargo)
        {
            var result = new Result();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    connection.Update(Cargo);
                    result.Message = "Cargo actualizada correctamente.";
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error actualizando Cargo.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<Cargo> InsCargo(Cargo Cargo)
        {
            var result = new Result<Cargo>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    Cargo.Id = connection.Insert(Cargo);
                    result.Message = "Cargo creada correctamente.";
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error creando Cargo.";
                result.Exception = ex;
            }
            return result;
        }
    }
}
