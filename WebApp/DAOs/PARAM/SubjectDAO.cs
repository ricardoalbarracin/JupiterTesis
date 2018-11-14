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
    public class SubjectDAO : BaseDAO, ISubjectDAOService
    {
        public SubjectDAO(IDapperAdapter dapper) : base(dapper)
        {
        }
       
        public Result<List<Subject>> GetListSubjects()
        {
            var result = new Result<List<Subject>>();
            try
            {
                using (var connection = _dapperAdapter.Get())
                {
                    result.Data = connection.GetAll<Subject>().ToList();

                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando Subjectas.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<Subject> GetSubjectById(long id)
        {
            var result = new Result<Subject>();
            try
            {
                using (var connection = _dapperAdapter.Get())
                {
                    result.Data = connection.Get<Subject>(id);
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando Subjecta.";
                result.Exception = ex;
            }
            return result;
        }

        public Result UpdSubject(Subject subject)
        {
            var result = new Result();
            try
            {
                using (var connection = _dapperAdapter.Get())
                {
                    connection.Execute(@"UPDATE core.subject 
                                        SET institution_id = @InstitutionId,
                                        code = @Code,
                                        description = @Description,
                                        active = @Active
                                        WHERE id = @Id;", subject);
                    result.Message = "Subjecta actualizada correctamente.";
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error actualizando Subjecta.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<Subject> InsSubject(Subject subject)
        {
            var result = new Result<Subject>();
            try
            {
                subject.InstitutionId = 1;
                using (var connection = _dapperAdapter.Get())
                {
                    subject.Id = connection.QuerySingle<int>(@"INSERT INTO core.subject 
                                                            (
                                                                        institution_id,
                                                                        code,
                                                                        description,
                                                                        active
                                                            )
                                                            VALUES
                                                            ( 
                                                                        @InstitutionId,
                                                                        @Code,
                                                                        @Description,
                                                                        @Active
                                                            )
                                                            returning id;", subject);
                     
                    result.Message = "Subjecta creado correctamente.";
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error creando Subjecta.";
                result.Exception = ex;
            }
            return result;
        }

       
    }
}
