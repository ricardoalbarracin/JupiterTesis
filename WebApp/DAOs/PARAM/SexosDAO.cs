﻿using Core.Models.PARAM;
using Core.Models.Utils;
using Core.Services.PARAM;
using Core.Services.Utils;
using DAOs.Utils;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAOs.PARAM
{
    public class GendersDAO : BaseDAO, IGenderDAOService
    {
        public GendersDAO(IDapperAdapter dapper) : base(dapper)
        {
        }

        public Result<List<Gender>> GetListGenders()
        {
            var result = new Result<List<Gender>>();
            try
            {
                using (var connection = _dapperAdapter.Get())
                {
                    result.Data = connection.GetAll<Gender>().ToList();

                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando listado de sexos.";
                result.Exception = ex;
            }
            return result;
        }

        
    }
}
