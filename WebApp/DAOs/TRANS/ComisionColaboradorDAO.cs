using Core.Models.TRANS;
using Core.Models.Utils;
using Core.Services.TRANS;
using Core.Services.Utils;
using DAOs.Utils;
using Dapper.Contrib.Extensions;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Models.PARAM;

namespace DAOs.TRANS
{
    public class ComisionColaboradorDAO: BaseDAO, IColaboradorComisionDAOService
    {
        public ComisionColaboradorDAO(IDapperAdapter dapper) : base(dapper)
        {
        }

        public Result<List<ComisionColaborador>> GetListComisiones(long? personaId)
        {
            var result = new Result<List<ComisionColaborador>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.Query<ComisionColaborador>(@" 
                                              SELECT cc.Id AS Id, p.Id AS PersonaId, 
                                             concat(p.PrimerNombre, ' ', p.PrimerApellido) AS NombreSolicitante, 
                                             cc.FechaInicio, cc.CantidadDias, cc.FechaFinalizacion, cc.FechaSolicitud, cc.ValorComision AS ValorComision,
                                             concat(p1.PrimerNombre, ' ', p1.PrimerApellido) AS NombreColaborador,
                                             cc.Estado
                                             FROM CORE.ColaboradorComision cc 
                                             INNER JOIN ADMIN.Personas p ON p.Id = cc.PersonaId
                                             INNER JOIN ADMIN.personas p1 ON p1.Id = cc.ColaboradorId
                                             WHERE cc.PersonaId = @id",
                                              new { id = personaId }).ToList();
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando comisiones.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<List<ListGeneral>> GetlistColaboradoresByProyectId(long ProyectoId)
        {
            var result = new Result<List<ListGeneral>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.Query<ListGeneral>(@" 
                                              SELECT per.id AS Id, concat(PrimerNombre, ' ', PrimerApellido) AS Nombre , 
                                                     c.ValorComision as Valor
                                              FROM ADMIN.Personas per
                                              INNER JOIN CORE.Cargos c ON C.Id = per.cargoID 
                                               WHERE DependenciaId IN(            
                                                 SELECT id FROM CORE.Dependencias WHERE PadreId IN 
                                                    (SELECT d.Id FROM core.Proyectos p
                                                     INNER JOIN CORE.Dependencias d ON p.DependenciaId = d.Id
                                                     WHERE p.Id = @id))",
                                              new { id = ProyectoId }).ToList();
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando Colaboradores.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<ComisionColaborador> InsComisionColaborador(ComisionColaborador comisionColaborador)
        {
            var result = new Result<ComisionColaborador>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    comisionColaborador.Id = connection.Insert(comisionColaborador);
                    result.Data = comisionColaborador;
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error creando nueva comisión.";
                result.Exception = ex;
            }
            return result;
        }
        #region informacionConsecutivo

        public int GetConsecutivo(int tipo)
        {
            int valConsecutivo = 0;
            var result = new Result<Consecutivo>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.QueryFirst<Consecutivo>(@" SELECT Valor +1 as Valor FROM ADMIN.consecutivos WHERE tipo= @type",
                                                          new { type = tipo });
                    result.Success = true;
                }
                valConsecutivo = result.Data.valor;
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando consecutivo.";
                result.Exception = ex;
            }
            return valConsecutivo;
        }

        public int UpdConsecutivo(int tipo, int valor)
        {
            int valConsecutivo = 0;
            var result = new Result<Consecutivo>();           
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data.valor = connection.Execute(@" UPDATE ADMIN.Consecutivos
                                                                    SET 
	                                                                     Valor = @value
                                                                    WHERE Tipo = @type",
                                                          new { type = tipo, value = valor });                  
                }
                valConsecutivo = result.Data.valor;
            }
            catch (Exception ex)
            {
                result.Message = "Error actualizando consecutivo.";
                result.Exception = ex;
            }
            return valConsecutivo;
        }

        #endregion

        #region updateComision
        public Result<ComisionColaborador> UpdSolicitudComision(long idComision)
        {
            var result = new Result<ComisionColaborador>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.QueryFirst<ComisionColaborador>(@" SELECT cc.Id AS Id, p.Id AS PersonaId, 
                                           concat(p.PrimerNombre, ' ', p.PrimerApellido) AS NombreSolicitante, 
                                           cc.FechaInicio, 
                                           cc.CantidadDias, 
                                           cc.FechaFinalizacion, 
                                           cc.FechaSolicitud, cc.ValorComision AS ValorComision,
                                           concat(p1.PrimerNombre, ' ', p1.PrimerApellido) AS NombreColaborador,
                                           cc.Estado, cc.EstadoLegalizacion, 
                                           d.Id AS Destino, 
                                           d.PadreId AS DepartamentoDestino,
                                           o.Id AS Origen,  
                                           o.PadreId AS DepartamentoOrigen  ,  
                                           pr.Descripcion, pr.Id AS ProyectoId, 
                                           cc.ValorComision,
                                           cc.Justificacion, cc.Estado
                                             FROM CORE.ColaboradorComision cc 
                                             INNER JOIN ADMIN.Personas p ON p.Id = cc.PersonaId
                                             INNER JOIN ADMIN.personas p1 ON p1.Id = cc.ColaboradorId
                                             INNER JOIN core.Proyectos pr ON pr.Id = cc.ProyectoId
                                             INNER JOIN ADMIN.Divipola d ON d.Id = cc.Destino
                                             INNER JOIN ADMIN.Divipola o ON o.Id = cc.Origen
                                             WHERE cc.Id  = @id",
                                              new { id = idComision });
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando información de comisión.";
                result.Exception = ex;
            }
            return result;
        }

        public Result UpdSolicitudComision(ComisionColaborador comisionColaborador)
        {
            var result = new Result();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    connection.Update(comisionColaborador);
                    result.Message = "Comisiòn actualizada correctamente.";
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error actualizando comisiòn.";
                result.Exception = ex;
            }
            return result;
        }
        #endregion
    }
}


