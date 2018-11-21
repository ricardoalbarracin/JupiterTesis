using Core.Models.PARAM;
using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.PARAM
{
    public interface IDivipolaDAOService
    {
        Result<List<Divipola>> GetListDeptos();

        Result<List<Divipola>> GetListMunicipios(long padreId);
    }
}

