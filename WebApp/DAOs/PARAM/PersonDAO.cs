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
    public class PersonDAO : BaseDAO, IPersonDAOService
    {
        public PersonDAO(IDapperAdapter dapper) : base(dapper)
        {
        }
        public Result<List<Person>> GetListPersons()
        {
            var result = new Result<List<Person>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.GetAll<Person>().ToList();

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

        public Result<Person> GetPersonById(long id)
        {
            var result = new Result<Person>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.Get<Person>(id);
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

        public Result UpdPerson(Person person)
        {
            var result = new Result();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    connection.Update(person);
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

        public Result<Person> InsPerson(Person person)
        {
            var result = new Result<Person>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    person.Id = connection.Insert(person);
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
