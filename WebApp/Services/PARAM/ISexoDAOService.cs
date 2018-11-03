using Core.Models.PARAM;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;


namespace Core.Services.PARAM
{
    public interface ISexoDAOService
    {
        Result<List<Sexos>> GetListSexos();
    }
}