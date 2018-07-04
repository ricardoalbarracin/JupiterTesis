using Core.Models.ADMIN;
using Core.Models.CORE;
using Core.Models.SEG;
using Core.Models.Utils;
using Core.Services.ADMIN;
using Core.Services.CORE;
using Core.Services.SEG;
using Core.Services.Utils;
using DAOs.Utils;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAOs.CORE
{
    public class ColaboradorDAO : BaseDAO, IColaboradorDAOService
    {
        public ColaboradorDAO(IDapperAdapter dapper) : base(dapper)
        {
        }

        public Result<List<ColaboradorGrid>> ListColaboradoresGrid()
        {
            var result = new Result<List<ColaboradorGrid>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.Query<ColaboradorGrid>(@"SELECT c.Id,
	                                                                    p.Documento,
	                                                                    p.PrimerNombre,
	                                                                    p.SegundoNombre,
	                                                                    p.FechaNacimiento,
	                                                                    p.PrimerApellido,
	                                                                    p.SegundoApellido,
	                                                                    cargo.Descripcion as Cargo
                                                                    FROM ADMIN.Personas p
                                                                    INNER JOIN CORE.Colaboradores c ON(p.Id = c.PersonaId)
                                                                    INNER JOIN CORE.Cargos Cargo ON(Cargo.Id = c.CargoId  )").AsList();
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando usuarios";
                result.Exception = ex;
            }

            return result;
        }

       
    }
}
