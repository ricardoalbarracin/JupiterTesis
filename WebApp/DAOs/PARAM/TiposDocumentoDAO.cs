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
    public class TiposDocumentoDAO : BaseDAO, ITiposDocumentoDAOService
    {
        public TiposDocumentoDAO(IDapperAdapter dapper) : base(dapper)
        {
        }    

        public Result<List<TipoDocumento>> GetListTiposDocumento()
        {
            var result = new Result<List<TipoDocumento>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.GetAll<TipoDocumento>().ToList();

                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando tipos de documento.";
                result.Exception = ex;
            }
            return result;
        }

    }
}
