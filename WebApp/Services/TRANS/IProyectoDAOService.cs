using Core.Models.TRANS;
using Core.Models.Utils;
using System.Collections.Generic;

namespace Core.Services.TRANS
{
    public interface IProyectoDAOService
    {
        Result<List<Proyecto>> GetListProyectos();

        Result<Proyecto> GetProyectoById(long id);

        Result UpdProyecto(Proyecto Proyecto);

        Result<Proyecto> InsProyecto(Proyecto Proyecto);
    }
}
