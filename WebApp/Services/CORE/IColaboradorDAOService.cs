using Core.Models.ADMIN;
using Core.Models.CORE;
using Core.Models.Utils;
using System.Collections.Generic;

namespace Core.Services.CORE
{
    public interface IColaboradorDAOService
    {
        Result<ColaboradorEdit> GetColaboradorEditById(int id);
        
    }
}
