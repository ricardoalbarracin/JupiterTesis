using Core.Models.PARAM;
using Core.Models.TRANS;
using Core.Models.SEG;
using Core.Models.Utils;
using Core.Services.PARAM;
using Core.Services.TRANS;
using Core.Services.SEG;
using Core.Services.Utils;
using DAOs.Utils;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAOs.TRANS
{
    public class ColaboradorDAO : BaseDAO, IColaboradorDAOService
    {
        public ColaboradorDAO(IDapperAdapter dapper) : base(dapper)
        {
        }

        

        public Result<ColaboradorEdit> GetColaboradorEditById(int id)
        {

            var result = new Result<ColaboradorEdit>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    // Obtiene informacion basica del usuario
                    var colaboradorEdit = new ColaboradorEdit();
                    //colaboradorEdit.Contratos = connection.Get<Contrato>(id);

                    colaboradorEdit.Persona = connection.Get<Persona>(colaboradorEdit.Persona.Id);

                    // Actualiza informacion del objeto Usuario.
                    result.Data = colaboradorEdit;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando colaborador";
                result.Exception = ex;
            }

            return result;

        }


    }
}
