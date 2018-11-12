using Core.Models.PARAM;
using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.PARAM
{
    public interface IStudentDAOService
    {
        Result<List<StudentView>> GetListStudents();

        Result<Student> GetStudentById(long id);

        Result<Student> GetStudentByPersonId(long id);

        Result UpdStudent(Student student);

        Result<Student> InsStudent(Student student);
        
    }
}
