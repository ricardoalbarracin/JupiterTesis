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
    public class DocumentTypeDAO : BaseDAO, IDocumentTypeDAOService
    {
        public DocumentTypeDAO(IDapperAdapter dapper) : base(dapper)
        {
        }    

        public Result<List<DocumentType>> GetListDocumentTypes()
        {
            var result = new Result<List<DocumentType>>();
            try
            {
                using (var connection = _dapperAdapter.Get())
                {
                    result.Data = connection.GetAll<DocumentType>().ToList();

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
