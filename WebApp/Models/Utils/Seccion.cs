using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Utils
{
    public class Seccion<T>
    {
        public string Container { get; set; }
        public T ModelData { get; set; }

        public string ModelType
        {
            get
            {
                return ModelData.GetType().ToString();
            }
        }
    }
}
