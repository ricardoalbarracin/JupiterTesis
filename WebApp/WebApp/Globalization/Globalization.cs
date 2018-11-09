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
        private LocalizationCulture _culture;
        ILocalizationRecordDAOService _localizationRecordDAOService;
        IDistributedCache _distributedCache;

        private List<LocalizationRecord> _LocalizationRecords
        {
            get
            {
                return _localizationRecordDAOService.GetListLocalizationRecords().Data;
            }
        }

        public DbStringLocalizer(ILocalizationRecordDAOService localizationRecordDAOService, IDistributedCache distributedCache) : this( CultureInfo.CurrentUICulture, localizationRecordDAOService) 
        {
            _distributedCache = distributedCache;
        }

        public DbStringLocalizer(  CultureInfo cultureInfo, ILocalizationRecordDAOService localizationRecordDAOService)
        {
            _localizationRecordDAOService = localizationRecordDAOService;
            var culture = _localizationRecordDAOService.GetListLocalizationCultures().Data.Where(m => string.Compare(m.Code, cultureInfo.Name, StringComparison.Ordinal) == 0);
            _culture = culture?.Count() > 0 ?  culture.First() : _localizationRecordDAOService.GetListLocalizationCultures().Data[0];
        }

        public DbStringLocalizer(CultureInfo cultureInfo)
        {
            _culture = _localizationRecordDAOService.GetListLocalizationCultures().Data.Where(m => m.Code == cultureInfo.Name).First();
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
            if(CultureInfo.CurrentCulture.Name != _culture.Code)
            {
                var culture = _localizationRecordDAOService.GetListLocalizationCultures().Data.Where(m => m.Code.CompareTo(CultureInfo.CurrentCulture.Name) == 0);
                _culture = culture?.Count() > 0 ? culture.First() : _localizationRecordDAOService.GetListLocalizationCultures().Data[0];
            }
            var data = _LocalizationRecords.FirstOrDefault(m => m.Code == name && m.LocalizationClutureId == _culture.Id);
            if (data == null )
                return $"{name}({_culture.Code})" ;
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
