using Core.Models.CORE;
using Core.Models.Utils;
using System.Collections.Generic;

namespace Core.Services.CORE
{
    public interface IColaboradorDAOService
    {
        Result<List<ColaboradorGrid>> ListColaboradoresGrid();
    }
}
