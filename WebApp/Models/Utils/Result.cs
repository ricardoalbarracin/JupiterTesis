using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Utils
{
    

    public class Result
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }

        public Result()
        {
            Success = false;
            Message = string.Empty;
            Exception = null;
        }
    }
    public class Result<T>: Result
    {
        public T Data { get; set; }

        public Result():base()
        {
            
        }
    }
}
