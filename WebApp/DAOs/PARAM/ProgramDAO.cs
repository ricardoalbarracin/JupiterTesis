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
    public class ProgramDAO : BaseDAO, IProgramDAOService
    {
        public ProgramDAO(IDapperAdapter dapper) : base(dapper)
        {
        }
       
        public Result<List<Program>> GetListPrograms()
        {
            var result = new Result<List<Program>>();
            try
            {
                using (var connection = _dapperAdapter.Get())
                {
                    result.Data = connection.GetAll<Program>().ToList();

                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando Programas.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<Program> GetProgramById(long id)
        {
            var result = new Result<Program>();
            try
            {
                using (var connection = _dapperAdapter.Get())
                {
                    result.Data = connection.Get<Program>(id);
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando Programa.";
                result.Exception = ex;
            }
            return result;
        }

        public Result UpdProgram(Program program)
        {
            var result = new Result();
            try
            {
                using (var connection = _dapperAdapter.Get())
                {
                    connection.Execute(@"UPDATE core.program 
                                        SET institution_id = @InstitutionId,
                                        code = @Code,
                                        description = @Description,
                                        active = @Active
                                        WHERE id = @Id;", program);
                    result.Message = "Programa actualizada correctamente.";
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error actualizando Programa.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<Program> InsProgram(Program program)
        {
            var result = new Result<Program>();
            try
            {
                program.InstitutionId = 1;
                using (var connection = _dapperAdapter.Get())
                {
                    program.Id = connection.QuerySingle<int>(@"INSERT INTO core.program 
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
                                                            returning id;", program);
                     
                    result.Message = "Programa creado correctamente.";
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error creando Programa.";
                result.Exception = ex;
            }
            return result;
        }

       
    }
}
