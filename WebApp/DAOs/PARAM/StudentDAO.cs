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
using System.Transactions;

namespace DAOs.PARAM
{
    public class StudentDAO : BaseDAO, IStudentDAOService
    {
        IPersonDAOService _personDAOService;
        public StudentDAO(IDapperAdapter dapper, IPersonDAOService personDAOService) : base(dapper)
        {
            _personDAOService = personDAOService;
        }

        public Result<List<StudentView>> GetListStudents()
        {
            var result = new Result<List<StudentView>>();
            try
            {
                using (var connection = _dapperAdapter.Get())
                {
                    result.Data = connection.Query<StudentView>(@"SELECT s.id,
                                   p.document,
                                   p.firts_name,
                                   p.second_name,
                                   p.birth_date,
                                   p.surname,
                                   p.second_surname
                            FROM param.Person p
                                INNER JOIN core.student s ON(p.id = s.person_id) ").AsList();
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando estudiantes.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<Student> GetStudentById(long id)
        {
            var result = new Result<Student>();
            try
            {
                using (var connection = _dapperAdapter.Get())
                {
                    var usuarioEdit = new Student();
                    result.Data = connection.Get<Student>(id);
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando estudiante";
                result.Exception = ex;
            }

            return result;
        }

        public Result<Student> GetStudentByPersonId(long PersonId)
        {
            var result = new Result<Student>();
            try
            {
                using (var connection = _dapperAdapter.Get())
                {
                    result.Data = connection.Query<Student>(@"SELECT s.id,
                                                            s.person_id,
                                                            s.institution_id
                                                            FROM core.studetnt s
                                                                where  person_id = @PersonId", new { PersonId }).First();
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando estudiante.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<Student> InsStudent(Student student)
        {
            var result = new Result<Student>();
            try
            {
                using (var connection = _dapperAdapter.Get())
                {
                    student.Id = connection.QuerySingle<int>(@"INSERT INTO core.student
	                                                            (  person_id, institution_id, academic_grade_id) 
                                                            VALUES ( '@PersonId', '@InstitutionId', '@AcademicGradeId' )
                                                            returning id;", student);

                    result.Message = "estudiante creado correctamente.";
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

        public Result UpdStudent(Student student)
        {
            var result = new Result();
            try
            {
                using (var connection = _dapperAdapter.Get())
                {

                    connection.Execute(@"UPDATE core.student 
                                        SET 
                                        person_id = @PersonId,
                                        institution_id = @InstitutionId,
                                        academic_grade_id = @AcademicGradeId
                                        WHERE id = @Id;", student);
                }
                result.Success = true;
                result.Message = "se ha actualizado correctacmente el estudiante.";
            }
            catch (Exception ex)
            {
                result.Message = "Error actualizando estudiante";
                result.Exception = ex;
            }
            return result;
        }


    }
}
