using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Extensions.Localization;

namespace WebApp.Resources
{
    public class DbStringLocalizerFactory : IStringLocalizerFactory
    {
       

        public DbStringLocalizerFactory()
        {

        }
        public IStringLocalizer Create(Type resourceSource)
        {
            return new DbStringLocalizer();
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new DbStringLocalizer();
        }
    }

    public class DbStringLocalizer : IStringLocalizer
    {
        private string _cultureName;

        public DbStringLocalizer( ) : this( CultureInfo.CurrentUICulture) { }

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

            return _cultureName;
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new DbStringLocalizer(culture);
        }
        public IEnumerable<LocalizedString> GetAllStrings(bool includeAncestorCultures)
        {
            throw new NotImplementedException();
        }

        IEnumerable<LocalizedString> IStringLocalizer.GetAllStrings(bool includeParentCultures)
        {
            throw new NotImplementedException();
        }
    }
}
