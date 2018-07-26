using Core.Models.PARAM;
using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.PARAM
{
    public interface IRubroDAOService
    {
        Result<List<Rubro>> GetListRubros();

        Result<Rubro> GetRubroById(long id);

        Result UpdRubro(Rubro Rubro);

        Result<Rubro> InsRubro(Rubro Rubro);


    }
}
