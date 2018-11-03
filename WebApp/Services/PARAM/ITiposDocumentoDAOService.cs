using Core.Models.PARAM;
using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.PARAM
{
    public interface ITiposDocumentoDAOService
    {
        Result<List<TipoDocumento>> GetListTiposDocumento();      
    }
}
