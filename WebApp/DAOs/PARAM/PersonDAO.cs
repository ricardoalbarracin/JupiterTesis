using Core.Models.PARAM;
using Core.Models.Utils;
using Core.Services.PARAM;
using Core.Services.Utils;
using DAOs.Utils;
using Dapper;
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
                using (var connection = _dapperAdapter.Get())
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
                using (var connection = _dapperAdapter.Get())
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
                using (var connection = _dapperAdapter.Get())
                {
                    connection.Execute(@"UPDATE param.person 
                                        SET document_type_id = @DocumentTypeId,
                                        document = @Document,
                                        expedition_date = @ExpeditionDate,
                                        firts_name = @FirtsName,
                                        second_name = @SecondName,
                                        surname = @Surname,
                                        second_surname = @SecondSurname,
                                        birth_date = @BirthDate,
                                        gender_id = @GenderId,
                                        place_birth_id = @PlaceBirthId,
                                        place_residence_id = @PlaceResidenceId,
                                        phone = @Phone,
                                        mobile = @Mobile,
                                        email = @Email,
                                        address = @Address WHERE id = @Id;", person);
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
                using (var connection = _dapperAdapter.Get())
                {
                    person.Id = connection.QuerySingle<int>(@"INSERT INTO param.person 
                                                            (
                                                                        document_type_id,
                                                                        document,
                                                                        expedition_date,
                                                                        firts_name,
                                                                        second_name,
                                                                        surname,
                                                                        second_surname,
                                                                        birth_date,
                                                                        gender_id,
                                                                        place_birth_id,
                                                                        place_residence_id,
                                                                        phone,
                                                                        mobile,
                                                                        email,
                                                                        address
                                                            )
                                                            VALUES
                                                            ( 
                                                                        @DocumentTypeId,
                                                                        @Document,
                                                                        @ExpeditionDate,
                                                                        @FirtsName,
                                                                        @SecondName,
                                                                        @Surname,
                                                                        @SecondSurname,
                                                                        @BirthDate,
                                                                        @GenderId,
                                                                        @PlaceBirthId,
                                                                        @PlaceResidenceId,
                                                                        @Phone,
                                                                        @Mobile,
                                                                        @Email,
                                                                        @Address
                                                            )
                                                            returning id;", person);
                     
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
