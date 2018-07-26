using Core.Models.TRANS;
using Core.Models.Utils;
using System.Collections.Generic;

namespace Core.Services.TRANS
{
    public interface IProyectoServiceBusiness
    {
        Result<ProyectoEdit> GetProyectoById(long id);


    }
}
