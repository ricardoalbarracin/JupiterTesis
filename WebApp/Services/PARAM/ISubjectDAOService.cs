using Core.Models.PARAM;
using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.PARAM
{
    public interface ISubjectDAOService
    {
        Result<List<Subject>> GetListSubjects();

        Result<Subject> GetSubjectById(long id);
        
        Result UpdSubject(Subject person);

        Result<Subject> InsSubject(Subject person);
    }
}
