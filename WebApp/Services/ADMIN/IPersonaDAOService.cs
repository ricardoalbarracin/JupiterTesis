using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.ADMIN
{
    public interface IPersonaDAOService
    {
        Result GetListPersonas();
        Result GetPersonaById(int id);
    }
}
