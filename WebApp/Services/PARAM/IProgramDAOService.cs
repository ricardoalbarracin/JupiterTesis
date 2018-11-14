using Core.Models.PARAM;
using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.PARAM
{
    public interface IProgramDAOService
    {
        Result<List<Program>> GetListPrograms();

        Result<Program> GetProgramById(long id);
        
        Result UpdProgram(Program person);

        Result<Program> InsProgram(Program person);
    }
}
