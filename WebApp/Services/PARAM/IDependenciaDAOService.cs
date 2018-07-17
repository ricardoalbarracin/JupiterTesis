using Core.Models.PARAM;
using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.PARAM
{
    public interface IDependenciaDAOService
    {
        Result<List<Dependencia>> GetListDependencias();

        Result<Dependencia> GetDependenciaById(int id);

        Result<Dependencia> InsDependencia(Dependencia dependencia);

        Result UpdDependencia(Dependencia dependencia);

        Result DelDependencia(int id);

        Result<DetalleDependencia> GetDetalleDependenciaById(int id);
    }
}
