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
    public class SexosDAO : BaseDAO, ISexoDAOService
    {
        public SexosDAO(IDapperAdapter dapper) : base(dapper)
        {
        }

        public Result<List<Sexos>> GetListSexos()
        {
            var result = new Result<List<Sexos>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.GetAll<Sexos>().ToList();

                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando listado de sexos.";
                result.Exception = ex;
            }
            return result;
        }

        
    }
}
