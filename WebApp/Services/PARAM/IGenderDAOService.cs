using Core.Models.PARAM;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;


namespace Core.Services.PARAM
{
    public interface IGenderDAOService
    {
        Result<List<Gender>> GetListGenders();
    }
}