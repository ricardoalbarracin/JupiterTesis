using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Core.Models.PARAM;
using Core.Services.PARAM;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;

namespace WebApp.Globalization
{
    public class DbStringLocalizerFactory : IStringLocalizerFactory
    {
        ILocalizationRecordDAOService _localizationRecordDAOService;
        IDistributedCache _distributedCache;

        public DbStringLocalizerFactory(IServiceScopeFactory scopeFactory, IDistributedCache distributedCache)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                _localizationRecordDAOService = scope.ServiceProvider.GetRequiredService<ILocalizationRecordDAOService>();
            }
            _distributedCache = distributedCache;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new DbStringLocalizer(_localizationRecordDAOService, _distributedCache);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new DbStringLocalizer(_localizationRecordDAOService, _distributedCache);
        }
    }

    public class DbStringLocalizer : IStringLocalizer
    {
        private string _cultureName;
        ILocalizationRecordDAOService _localizationRecordDAOService;
        IDistributedCache _distributedCache;

        private List<LocalizationRecord> _LocalizationRecords
        {
            get
            {
                var key = "CacheLocalizationRecords";
                var cacheLocalizationRecords = _distributedCache.GetString(key);
                if (cacheLocalizationRecords == null)
                {
                    var data = _localizationRecordDAOService.GetListLocalizationRecords().Data;
                    _distributedCache.SetString(key, JsonConvert.SerializeObject(data));
                    return data;
                }
                else
                {

                    return JsonConvert.DeserializeObject<List<LocalizationRecord>>(cacheLocalizationRecords);
                }

            }
        }

        public DbStringLocalizer(ILocalizationRecordDAOService localizationRecordDAOService, IDistributedCache distributedCache) : this( CultureInfo.CurrentUICulture) 
        {
            _localizationRecordDAOService = localizationRecordDAOService;
            _distributedCache = distributedCache;
        }

        public DbStringLocalizer(  CultureInfo cultureInfo)
        {
           
            _cultureName = cultureInfo.Name;
        }

        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name, value ?? name, resourceNotFound: value == null);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var format = GetString(name);
                var value = string.Format(format ?? name, arguments);
                return new LocalizedString(name, value, resourceNotFound: format == null);
            }
        }

        private string GetString(string name)
        {
            var data = _LocalizationRecords.FirstOrDefault(m => m.Code == name);
            if (data == null )
                return name;
            else
                return data.Description;

        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new DbStringLocalizer(culture);
        }
        public IEnumerable<LocalizedString> GetAllStrings(bool includeAncestorCultures)
        {
            throw new NotImplementedException();
        }
    }
}
