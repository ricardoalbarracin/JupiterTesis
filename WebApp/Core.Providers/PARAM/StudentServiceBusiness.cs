using AutoMapper;
using Core.Models.PARAM;
using Core.Models.SEG;
using Core.Models.Utils;
using Core.Services.PARAM;
using Core.Services.SEG;
using Core.Services.Utils;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Core.Business.PARAM
{
    public class StudentServiceBusiness : IStudentService
    {
        IPersonDAOService _personDAOService;
        IStudentDAOService _studentDAOService;

        public StudentServiceBusiness(IPersonDAOService personDAOService, IStudentDAOService studentDAOService)
        {
            _personDAOService = personDAOService;
            _studentDAOService = studentDAOService;
        }

        public Result<StudentEdit> GetStudentEditById(long id)
        {
            var result = new Result<StudentEdit>();
            var getPersonById = _personDAOService.GetPersonById(id);
            if (!getPersonById.Success)
            {
                result.Message = getPersonById.Message;
                return result;
            }

            var getStudentById = _studentDAOService.GetStudentById(id);
            if (!getStudentById.Success)
            {
                result.Message = getStudentById.Message;
                return result;
            }
            result.Data = new StudentEdit { Person = getPersonById.Data, Student = getStudentById.Data };

            result.Success = true;
            return result;
        }

        

        public Result UpdStudentPerson(IDictionary<string, object> dataSections)
        {
            var result = new Result();
            using (var transaction = new TransactionScope())
            {

                var person = dataSections["UpdPerson"] as Person;
                var updPerson = _personDAOService.UpdPerson(person);
                if (!updPerson.Success)
                {
                    result.Message = updPerson.Message;
                    return result;
                }

                var student = dataSections["UpdStudent"] as Student;
                var updStudent = _studentDAOService.UpdStudent(student);
                if (!updStudent.Success)
                {
                    result.Message = updStudent.Message;
                    return result;
                }
                transaction.Complete();
            }
            result.Success = true;
            result.Message = "se ha actualizado correctacmente el estudiante.";
            return result;
        }

        public Result InsStudentPerson(IDictionary<string, object> dataSections)
        {
            var result = new Result();
            using (var transaction = new TransactionScope())
            {

                var person = dataSections["InsPerson"] as Person;

                
                var getPersonByDocument = _personDAOService.GetPersonByDocument(person);
                if (!getPersonByDocument.Success)
                {
                    var insPerson = _personDAOService.InsPerson(person);
                    if (!insPerson.Success)
                    {
                        result.Message = insPerson.Message;
                        return result;
                    }
                    person.Id = insPerson.Data.Id;
                }
                else
                {
                    var getStudentByPersonId = _studentDAOService.GetStudentByPersonId(person.Id);
                    if (getStudentByPersonId.Success)
                    {
                        result.Message = "Esta persona ya se encuenta registrada como estudiante";
                        return result;
                    }

                    var updPerson = _personDAOService.UpdPerson(person);
                    if (!updPerson.Success)
                    {
                        result.Message = updPerson.Message;
                        return result;
                    }
                }

                var student = dataSections["InsStudent"] as Student;
                student.PersonId = person.Id;

                var updStudent = _studentDAOService.InsStudent(student);
                if (!updStudent.Success)
                {
                    result.Message = updStudent.Message;
                    return result;
                }
                transaction.Complete();
            }
            result.Success = true;
            result.Message = "se ha actualizado correctacmente el estudiante.";
            return result;
        }
    }
}
