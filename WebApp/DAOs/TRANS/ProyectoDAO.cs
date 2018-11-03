using Core.Models.PARAM;
using Core.Models.TRANS;
using Core.Models.Utils;
using Core.Services.TRANS;
using Core.Services.Utils;
using DAOs.Utils;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAOs.PARAM
{
    public class ProyectoDAO : BaseDAO, IProyectoDAOService
    {
        public ProyectoDAO(IDapperAdapter dapper) : base(dapper)
        {
        }
        public Result<List<Proyecto>> GetListProyectos()
        {
            var result = new Result<List<Proyecto>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.GetAll<Proyecto>().ToList();

                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando Proyectos.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<Proyecto> GetProyectoById(long id)
        {
            var result = new Result<Proyecto>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.Get<Proyecto>(id);
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando Proyecto.";
                result.Exception = ex;
            }
            return result;
        }

        public Result UpdProyecto(Proyecto Proyecto)
        {
            var result = new Result();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    connection.Update(Proyecto);
                    result.Message = "Proyecto actualizado correctamente.";
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error actualizando Proyecto.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<Proyecto> InsProyecto(Proyecto Proyecto)
        {
            var result = new Result<Proyecto>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    Proyecto.Id = connection.Insert(Proyecto);
                    result.Data = Proyecto;
                    result.Message = "Proyecto creado correctamente.";
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error creando Proyecto.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<List<ProyectosRubrosView>> GetListRubrosByProyectoId(long proyectoId)
        {
            var result = new Result<List<ProyectosRubrosView>>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.Query<ProyectosRubrosView>(@"SELECT p1.Id, 
                                                                        r.Codigo, 
                                                                        r.Descripcion,
                                                                        p1.RubroId,
                                                                        p1.ProyectoId,
                                                                        p1.Valor,
                                                                        p1.Saldo
                                                                       FROM  CORE.ProyectosRubros p1 
                                                                                INNER JOIN CORE.Rubros r ON(p1.RubroId = r.Id) where p1.ProyectoId=@Id", 
                                                          new { Id = proyectoId }).ToList();
                    

                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando Rubros.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<ProyectoRubro> GetRubroProyecto(long id)
        {
            var result = new Result<ProyectoRubro>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    result.Data = connection.Get<ProyectoRubro>(id);
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando Proyecto.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<ProyectoRubro> UpdRubroProyecto(ProyectoRubro proyectoRubro)
        {
            var result = new Result<ProyectoRubro>();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    proyectoRubro.Saldo = proyectoRubro.Valor;
                    //Consulta info saldo del proyecto - valor rubros
                    var sql = @"SELECT sum(pr.Valor) AS valor, p.PresupuestoAsignado as Saldo
                             FROM CORE.proyectos  p
                            INNER JOIN CORE.ProyectosRubros pr ON p.Id= pr.ProyectoId
                            INNER JOIN CORE.Rubros r ON r.Id = pr.RubroId
                            WHERE 
                            p.Id=@id
                            GROUP BY p.PresupuestoAsignado;";
                    var resultSaldo = connection.QueryFirst(sql, new { id = proyectoRubro.ProyectoId });
                    float saldoPresupuestoProyecto = (float)resultSaldo.Saldo - (float)resultSaldo.valor;
                    if (saldoPresupuestoProyecto - proyectoRubro.Valor > 0)
                    {
                        proyectoRubro.FechaModificacion = DateTime.Now;
                        proyectoRubro.FechaCreacion = DateTime.Now;
                        proyectoRubro.Saldo = proyectoRubro.Valor;
                        connection.Update(proyectoRubro);
                        result.Data = proyectoRubro;                        
                        result.Message = "Rubro actualizado correctamente.";
                        result.Success = true;
                    }
                    else
                    {
                        result.Message = "El presupuesto del proyecto ha sido alcanzado.";
                        result.Success = false;
                    }                    
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error actualizando rubro.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<ProyectoRubro> InsRubroProyecto(ProyectoRubro proyectoRubro)
        {
            var result = new Result<ProyectoRubro>();            
            try
            {  
                using (var connection = _dapperAdapter.Open())
                {
                    proyectoRubro.Saldo = proyectoRubro.Valor;

                    //Consulta info saldo del proyecto - valor rubros
                    var sql = @"SELECT PresupuestoAsignado FROM CORE.Proyectos 
                              WHERE Id=@id";
                    var valorProyecto = connection.QueryFirst(sql, new { id = proyectoRubro.ProyectoId });
                    sql = @"SELECT sum(pr.Valor) AS valor
                             FROM CORE.proyectos  p
                            INNER JOIN CORE.ProyectosRubros pr ON p.Id= pr.ProyectoId
                            INNER JOIN CORE.Rubros r ON r.Id = pr.RubroId
                            WHERE 
                            p.Id=@id
                            GROUP BY p.PresupuestoAsignado;";
                     var valorTotalRubros = connection.QueryFirstOrDefault(sql, new { id = proyectoRubro.ProyectoId });
                     float saldoPresupuestoProyecto = (float)valorTotalRubros.Saldo - (float)valorProyecto.PresupuestoAsignado;
                    
                    
                    if (saldoPresupuestoProyecto - proyectoRubro.Valor > 0)
                    {
                        proyectoRubro.Id = connection.Insert(proyectoRubro);
                        result.Data = proyectoRubro;
                        result.Message = "Rubro creado correctamente.";
                        result.Success = true;
                    }
                    else
                    {
                        result.Message = "El presupuesto del proyecto ha sido alcanzado.";
                        result.Success = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error creando rubro.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<Proyecto> GetValoresProyectos(long id)
        {
            var result = new Result<Proyecto>();
            var proyecto = new Proyecto();
            try
            {
                using (var connection = _dapperAdapter.Open())
                {
                    var sql = @"SELECT PresupuestoAsignado FROM CORE.Proyectos 
                              WHERE Id=@id";
                    var valorProyecto = connection.QueryFirst(sql, new { id = id });
                    sql = @"SELECT sum(pr.Valor) AS valor
                             FROM CORE.proyectos  p
                            INNER JOIN CORE.ProyectosRubros pr ON p.Id= pr.ProyectoId
                            INNER JOIN CORE.Rubros r ON r.Id = pr.RubroId
                            WHERE 
                            p.Id=@id
                            GROUP BY p.PresupuestoAsignado;";
                    var valorTotalRubros = connection.QueryFirstOrDefault(sql, new { id=id});
                   
                    if (valorTotalRubros != null)
                    {
                        proyecto.PresupuestosinAsignar = (float)valorTotalRubros.Saldo - (float)valorProyecto.PresupuestoAsignado;
                    }
                    else
                        proyecto.PresupuestosinAsignar = (float)valorProyecto.PresupuestoAsignado;
                    proyecto.PresupuestoAsignado = (float)valorProyecto.PresupuestoAsignado;
                    result.Data = proyecto;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando información del proyecto.";
                result.Exception = ex;
            }
            
            return result;
        }
    }
}
