using System;
using System.Collections.Generic;
using System.Text;
using Core.Models.TRANS;
using Core.Models.Utils;

namespace Core.Services.TRANS
{
    public interface IColaboradorComisionDAOService
    {
        Result<List<Proyecto>> GetListComisiones();
    }
}
