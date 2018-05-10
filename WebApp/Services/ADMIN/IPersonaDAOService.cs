using Core.Models.ADMIN;
using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.ADMIN
{
    public interface IPersonaDAOService
    {
        Result<IEnumerable<Persona>> GetListPersonas();
        Result<Persona> GetPersonaById(long id);
    }
}
