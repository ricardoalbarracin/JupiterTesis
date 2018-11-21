using Core.Models.PARAM;
using Core.Models.Utils;
using Core.Services.PARAM;
using Core.Services.Utils;
using DAOs.Utils;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAOs.PARAM
{
    public class DivipolaDAO : BaseDAO, IDivipolaDAOService
    {
        public DivipolaDAO(IDapperAdapter dapper) : base(dapper)
        {
        }

        public Result<List<Divipola>> GetListDeptos()
        {
            var result = new Result<List<Divipola>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.Query<Divipola>(@"
                                                            SELECT Id, Nombre
                                                            FROM ADMIN.Divipola
                                                            WHERE TDivipolaId = 1").ToList();
                    result.Success = true;
                }
            }   
            catch (Exception ex)
            {
                result.Message = "Error consultando departamentos.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<List<Divipola>> GetListMunicipios(long padreId)
        {
            var result = new Result<List<Divipola>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.Query<Divipola>(@" SELECT Id, Nombre
                                                            FROM ADMIN.Divipola
                                                            WHERE PadreId=@Id",
                                                          new { Id = padreId }).ToList();


                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando municipios.";
                result.Exception = ex;
            }
            return result;
        }
       
    }
}
