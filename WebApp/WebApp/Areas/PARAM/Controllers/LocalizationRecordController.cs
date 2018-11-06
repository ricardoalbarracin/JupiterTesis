using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models.PARAM;
using Core.Models.SEG;
using Core.Models.Utils;
using Core.Services.PARAM;
using Core.Services.SEG;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using WebApp.Helpers;

namespace WebApp.Areas.PARAM.Controllers
{
    [Area("PARAM")]
    public class LocalizationRecordController : Controller
    {
        ILocalizationRecordDAOService _localizationRecordService;
        IDistributedCache _distributedCache;

        public LocalizationRecordController(ILocalizationRecordDAOService localizationRecordService, IDistributedCache distributedCache)
        {
            _localizationRecordService = localizationRecordService;
            _distributedCache = distributedCache;
           
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetListLocalizationRecords([DataSourceRequest] DataSourceRequest request)
        {
            var getListLocalizationRecords = _localizationRecordService.GetListLocalizationRecords();

            if (!getListLocalizationRecords.Success)
            {
                ModelState.AddModelError("Error", getListLocalizationRecords.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var localizationRecords = getListLocalizationRecords.Data as List<LocalizationRecord>;
            var key = "CacheLocalizationRecords";
            _distributedCache.SetString(key, JsonConvert.SerializeObject(localizationRecords));

            return Json(localizationRecords.ToDataSourceResult(request));
        }

        public ActionResult GetListLocalizationRecordTypes([DataSourceRequest] DataSourceRequest request)
        {
            var getListLocalizationRecordTypes = _localizationRecordService.GetListLocalizationRecordTypes();

            if (!getListLocalizationRecordTypes.Success)
            {
                ModelState.AddModelError("Error", getListLocalizationRecordTypes.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var localizationRecordTypes = getListLocalizationRecordTypes.Data;
            return Json(localizationRecordTypes.ToDataSourceResult(request));
        }





        public ActionResult GetListLocalizationCultures([DataSourceRequest] DataSourceRequest request)
        {
            var getListLocalizationCultures = _localizationRecordService.GetListLocalizationCultures();

            if (!getListLocalizationCultures.Success)
            {
                ModelState.AddModelError("Error", getListLocalizationCultures.Message);
                return Json(Enumerable.Empty<object>().ToDataSourceResult(request, ModelState));
            }
            var localizationCultures = getListLocalizationCultures.Data;

            return Json(localizationCultures.ToDataSourceResult(request));
        }
        [HttpGet]
        public ActionResult UpdLocalizationRecord(int id)
        {
            var getLocalizationRecordById = _localizationRecordService.GetLocalizationRecordById(id);

            if (!getLocalizationRecordById.Success)
            {
                ModelState.AddModelError("Error", getLocalizationRecordById.Message);
                return View(new LocalizationRecord());
            }
            return PartialView(getLocalizationRecordById.Data);
        }

        [HttpPost]
        public ActionResult UpdLocalizationRecord(LocalizationRecord localizationRecord)
        {
            var updLocalizationRecord = _localizationRecordService.UpdLocalizationRecord(localizationRecord);
            return new JsonResult(updLocalizationRecord);
        }

        [HttpGet]
        public ActionResult InsLocalizationRecord()
        {
            return PartialView(new LocalizationRecord());
        }

        [HttpPost]
        public ActionResult InsLocalizationRecord(LocalizationRecord localizationRecord)
        {
            var insLocalizationRecord = _localizationRecordService.InsLocalizationRecord(localizationRecord);
            return new JsonResult(insLocalizationRecord);
        }



    }
}