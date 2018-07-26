using AutoMapper;
using Core.Models.SEG;
using Core.Models.TRANS;
using Core.Models.Utils;
using Core.Services.PARAM;
using Core.Services.SEG;
using Core.Services.TRANS;
using Core.Services.Utils;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace Core.Business.TRANS
{
    public class ProyectosServiceBusiness : IProyectoServiceBusiness
    {
        IProyectoDAOService _ProyectoDAOService;
        IRubroDAOService _RubroDAOService;

        public ProyectosServiceBusiness(IProyectoDAOService proyectoDAOService, IRubroDAOService rubroDAOService)
        {
            _ProyectoDAOService = proyectoDAOService;
            _RubroDAOService = rubroDAOService;

        }

        public Result<ProyectoEdit> GetProyectoById(long id)
        {
            var result = new Result<ProyectoEdit>();

            var getProyectoById = _ProyectoDAOService.GetProyectoById(id);
            if (!getProyectoById.Success)
            {
                result.Message = getProyectoById.Message;
                return result;
            }

            var getListRubrosByProyectoId = _ProyectoDAOService.GetListRubrosByProyectoId(id);
            if (!getListRubrosByProyectoId.Success)
            {
                result.Message = getListRubrosByProyectoId.Message;
                return result;
            }

            result.Success = true;
            result.Data = new ProyectoEdit { Proyecto = getProyectoById.Data, Rubros = getListRubrosByProyectoId.Data };
            return result;
        }
    
    }
}
