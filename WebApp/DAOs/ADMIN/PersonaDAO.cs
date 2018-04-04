using Core.Models.ADMIN;
using Core.Models.SEG;
using Core.Models.Utils;
using Core.Services.ADMIN;
using Core.Services.SEG;
using Core.Services.Utils;
using DAOs.Utils;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAOs.ADMIN
{
    public class PersonaDAO : BaseDAO, IPersonaService
    {
        public PersonaDAO(IDapperAdapter dapper) : base(dapper)
        {
        }
        public Result GetListPersonas()
        {
            var result = new Result();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.GetAll<Persona>();
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando personas.";
                result.Exception = ex;
            }
            return result;
        }
       
    }
}
