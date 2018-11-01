using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Reflection;

namespace WebApp.Resources
{
    public class LocService
    {
        private readonly IStringLocalizer _localizer;

        public LocService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResource", assemblyName.Name);
        }

        public LocalizedString GetLocalizedHtmlString(string key)
        {
            var aa = CultureInfo.CurrentCulture;
            return _localizer[key];
        }


    }
}