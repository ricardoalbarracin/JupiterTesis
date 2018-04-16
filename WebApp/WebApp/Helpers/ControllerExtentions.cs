using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Loader;
using System.Threading.Tasks;

namespace WebApp.Helpers
{
    public static class ControllerExtentions
    {
        /// <summary>
        /// Metodo que convierte una estructura de secciones en formato JSON 
        /// a un diccionario de modelos
        /// </summary>
        /// <param name="sections">secciones</param>
        /// <returns>diccionario de secciones</returns>
        public static IDictionary<string, object> ConvertSectionsToModels(this Controller controller, IDictionary<string, IDictionary<string, string>> sections)
        {
            var sectionsConverted = new Dictionary<string, object>();
            var modelAssembly = AssemblyLoadContext.Default.LoadFromAssemblyName(new System.Reflection.AssemblyName("Core.Models"));
            foreach (var item in sections.Values)
            {
                // Validaciones
                
                var modelType = modelAssembly.GetType(item["Model"].ToString());

                var modelDeserialized = JsonConvert.DeserializeObject(item["DataForm"].ToString(), modelType);

                var vc = new ValidationContext(modelDeserialized, null, null);
                var isValid = Validator.TryValidateObject(modelDeserialized, vc, null, true);
                if (!isValid)
                {
                    var ex = new Exception("Modelo invalido");
                    throw ex;
                }

                sectionsConverted[item["SectionId"].ToString()] = modelDeserialized;
            }

            return sectionsConverted;
        }
    }
}
