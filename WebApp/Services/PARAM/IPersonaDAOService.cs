using Core.Models.PARAM;
using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.PARAM
{
    public interface IPersonaDAOService
    {
        Result<List<Persona>> GetListPersonas();

        Result<Persona> GetPersonaById(long id);

        Result UpdPersona(Persona persona);

        Result<Persona> InsPersona(Persona persona);
    }
}
