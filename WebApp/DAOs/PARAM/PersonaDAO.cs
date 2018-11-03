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
    public class PersonaDAO : BaseDAO, IPersonaDAOService
    {
        public PersonaDAO(IDapperAdapter dapper) : base(dapper)
        {
        }
        public Result<List<Persona>> GetListPersonas()
        {
            var result = new Result<List<Persona>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.GetAll<Persona>().ToList();

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

        public Result<Persona> GetPersonaById(long id)
        {
            var result = new Result<Persona>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.Get<Persona>(id);
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando persona.";
                result.Exception = ex;
            }
            return result;
        }

        public Result UpdPersona(Persona persona)
        {
            var result = new Result();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    connection.Update(persona);
                    result.Message = "Persona actualizada correctamente.";
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error actualizando persona.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<Persona> InsPersona(Persona persona)
        {
            var result = new Result<Persona>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    persona.Id = connection.Insert(persona);
                    result.Message = "Persona creada correctamente.";
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error creando persona.";
                result.Exception = ex;
            }
            return result;
        }

       
    }
}
