using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.PARAM;
using Core.Models.TRANS;
using Core.Services.PARAM;
using Core.Services.TRANS;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using WebApp.Utils;

namespace WebApp.Areas.TRANS.Controllers
{
    [Area("TRANS")]
    public class SolicitudComisionController : Controller
    {
        IColaboradorComisionDAOService _ComisionColaborador;
        IDivipolaDAOService _Divipola;
        public SolicitudComisionController(IColaboradorComisionDAOService ComisionColaboradorService, IDivipolaDAOService DivipolaService)
        {
            _ComisionColaborador = ComisionColaboradorService;
            _Divipola = DivipolaService;
        }

        #region vistas

        public ActionResult SolicitudComision()
        {
            return View();
        }

        public ActionResult UpdSolicitudComision(int id)
        {
            var getComisionById = _ComisionColaborador.UpdSolicitudComision(id);
            ViewBag.Container = ControllerContext.RouteData.Values["action"].ToString();
            if (!getComisionById.Success)
            {
                ModelState.AddModelError("Error", getComisionById.Message);
                return View(new ComisionColaborador());
            }
            return PartialView(getComisionById.Data);
        }

        public ActionResult DesembolsoValor()
        {
            return View();
        }

        public ActionResult LegalizacionComision()
        {
            return View();
        }

        public ActionResult InsFacturas(int id)
        {
            var getComisionById = _ComisionColaborador.GetInfoComision(id);
            ViewBag.Container = ControllerContext.RouteData.Values["action"].ToString();
            if (!getComisionById.Success)
            {
                ModelState.AddModelError("Error", getComisionById.Message);
                return PartialView(new FacturasViewModel());
            }
            getComisionById.Data.SubTotal = getComisionById.Data.ValorComision;
            return PartialView(getComisionById.Data);
        }
        #endregion

        #region Cargainfo
        public ActionResult GetListComisiones([DataSourceRequest] DataSourceRequest request)
        {
            var usuario = HttpContext.Session.GetUser();
            var getListComisiones = _ComisionColaborador.GetListComisiones(usuario.PersonaId);

            if (!getListComisiones.Success)
            {
                ModelState.AddModelError("Error", getListComisiones.Message);


                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var Proyectos = getListComisiones.Data;
            return Json(Proyectos.ToDataSourceResult(request));
        }

        public ActionResult getListColaboradoresByProyectoId([DataSourceRequest] DataSourceRequest request, long ProyectoId)
        {
            var getListColaboradores = _ComisionColaborador.GetlistColaboradoresByProyectId(ProyectoId);

            if (!getListColaboradores.Success)
            {
                ModelState.AddModelError("Error", getListColaboradores.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var Colaboradores = new List<ListGeneral>();
            Colaboradores = getListColaboradores.Data;
            return Json(Colaboradores);
        }
        public ActionResult GetListDepartamentos([DataSourceRequest] DataSourceRequest request)
        {
            var getListDepartamentos = _Divipola.GetListDeptos();

            if (!getListDepartamentos.Success)
            {
                ModelState.AddModelError("Error", getListDepartamentos.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var Deptos = getListDepartamentos.Data;
            return Json(Deptos.ToDataSourceResult(request));
        }
        public ActionResult GetListMunicipios([DataSourceRequest] DataSourceRequest request, long padreId)
        {
            var getListMunicipios = _Divipola.GetListMunicipios(padreId);

            if (!getListMunicipios.Success)
            {
                ModelState.AddModelError("Error", getListMunicipios.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var mcpio = new List<Divipola>();
            mcpio = getListMunicipios.Data;
            return Json(mcpio);
        }

        public ActionResult GetListComisionesDesembolso([DataSourceRequest] DataSourceRequest request)
        {
            var usuario = HttpContext.Session.GetUser();
            var getListComisiones = _ComisionColaborador.GetListComisionesSinDesembolso();

            if (!getListComisiones.Success)
            {
                ModelState.AddModelError("Error", getListComisiones.Message);


                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var Proyectos = getListComisiones.Data;
            return Json(Proyectos.ToDataSourceResult(request));
        }

        public ActionResult GetListComisionesColaborador([DataSourceRequest] DataSourceRequest request)
        {
            var usuario = HttpContext.Session.GetUser();
            var getListComisiones = _ComisionColaborador.GetListComisionesColaborador(usuario.PersonaId);

            if (!getListComisiones.Success)
            {
                ModelState.AddModelError("Error", getListComisiones.Message);


                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var Proyectos = getListComisiones.Data;
            return Json(Proyectos.ToDataSourceResult(request));
        }

        public ActionResult getListConcept([DataSourceRequest] DataSourceRequest request)
        {
            var usuario = HttpContext.Session.GetUser();
            var getListConcept = _ComisionColaborador.getListConcept();

            if (!getListConcept.Success)
            {
                ModelState.AddModelError("Error", getListConcept.Message);


                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var Conceptos = getListConcept.Data;
            return Json(Conceptos.ToDataSourceResult(request));
        }


        #endregion

        #region crud
        [HttpGet]
        public ActionResult InsComision()
        {
            var usuario = HttpContext.Session.GetUser();
            ComisionColaborador comisionColaborador = new ComisionColaborador();
            comisionColaborador.PersonaId = usuario.PersonaId;
            comisionColaborador.NombreSolicitante = usuario.PrimerNombre + " " + usuario.PrimerApellido;

            return PartialView(comisionColaborador);
        }


        /// <summary>
        /// Inserta registro de nueva solicitud de comision
        /// </summary>
        /// <param name="comisionColaborador"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InsComision(ComisionColaborador comisionColaborador)
        {
            //se eenvia como tipo=1 para el formato de solictud de comision
            var consecutivo = _ComisionColaborador.GetConsecutivo(1);
            comisionColaborador.Consecutivo = consecutivo;
            comisionColaborador.EstadoLegalizacion = "Pendiente";
            comisionColaborador.Estado = "Pendiente";
            comisionColaborador.FechaSolicitud = DateTime.Now;
            var solComision = _ComisionColaborador.InsComisionColaborador(comisionColaborador);
            if (solComision.Data.Id > 0)
                _ComisionColaborador.UpdConsecutivo(1, comisionColaborador.Consecutivo);
            return Json(solComision);
        }

        [HttpPost]
        public ActionResult UpdSolicitudComision(ComisionColaborador comisionColaborador)
        {
            comisionColaborador.FechaSolicitud = DateTime.Now;
            var updComision = _ComisionColaborador.UpdSolicitudComision(comisionColaborador);
            return new JsonResult(updComision);

        }

        public ActionResult UpdDesembolsoComision(int id)
        {
            var getComisionById = _ComisionColaborador.UpdSolicitudComision(id);
            ViewBag.Container = ControllerContext.RouteData.Values["action"].ToString();
            if (!getComisionById.Success)
            {
                ModelState.AddModelError("Error", getComisionById.Message);
                return View(new ComisionColaborador());
            }

            ComisionColaborador comision = new ComisionColaborador();
            comision = getComisionById.Data;
            comision.Desembolso = 1;
            var updComision = _ComisionColaborador.UpdSolicitudComision(comision);
            return new JsonResult(updComision);

        }

        public ActionResult InsLegalizacion(int comisionId, Legalizaciones legalizaciones)
        {            
            legalizaciones.CantidadFacturas = legalizaciones.ListFacturas.Count();
            legalizaciones.ComisionId = comisionId;
            legalizaciones.Estado = "Pendiente";
            legalizaciones.FechaLegalizacion = DateTime.Now;
            legalizaciones.ValorFacturas = legalizaciones.ListFacturas.Select(x => x.ValorFactura).Sum();
            var result = _ComisionColaborador.InsLegalizacion(legalizaciones);
            if (result.Data.Id > 0)
            {
                foreach (FacturaIndividualViewModel facturas in legalizaciones.ListFacturas)
                {
                    facturas.LegalizacionId = 1;//result.Data.Id;
                    var resultFac = _ComisionColaborador.InsFacturas(facturas);
                    if (resultFac.Data.Id < 0)
                    {
                        return Json(resultFac);
                    }
                }
            }
            return Json(result);
        }

        #endregion
    }

}
