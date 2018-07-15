using Core.Models.PARAM;
using Core.Models.TRANS;
using Core.Models.Utils;
using System.Collections.Generic;

namespace Core.Services.TRANS
{
    public interface IColaboradorDAOService
    {
        Result<ColaboradorEdit> GetColaboradorEditById(int id);
        
    }
}
