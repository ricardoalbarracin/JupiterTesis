using Core.Models.PARAM;
using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.PARAM
{
    public interface IStudentService
    {
       

        Result<StudentEdit> GetStudentEditById(long id);

        Result UpdStudentPerson(IDictionary<string, object> dataSections);

        Result InsStudentPerson(IDictionary<string, object> dataSections);
    }
}
