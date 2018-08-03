using Core.Models.PARAM;
using Core.Models.TRANS;
using Core.Models.Utils;
using System.Collections.Generic;

namespace Core.Services.TRANS
{
    public interface IProyectoDAOService
    {
        Result<List<Proyecto>> GetListProyectos();

        Result<Proyecto> GetProyectoById(long id);

        Result<List<ProyectosRubrosView>> GetListRubrosByProyectoId(long ProyectoId);

        Result UpdProyecto(Proyecto Proyecto);

        Result<Proyecto> InsProyecto(Proyecto Proyecto);

        Result<ProyectoRubro> GetRubroProyecto(long id);

        Result UpdRubroProyecto(ProyectoRubro proyectoRubro);

        Result<ProyectoRubro> InsRubroProyecto(ProyectoRubro proyectoRubro);

    }
}
