using Core.Models.PARAM;
using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.PARAM
{
    public interface ICargoDAOService
    {
        Result<List<Cargo>> GetListCargos();

        Result<Cargo> GetCargoById(long id);

        Result UpdCargo(Cargo Cargo);

        Result<Cargo> InsCargo(Cargo Cargo);
    }
}
