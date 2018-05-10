using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Utils
{
    public sealed class Nothing
    {
    }
    public class Result<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public Exception Exception { get; set; }

        public Result()
        {
            Success = false;
            Message = string.Empty;
            Exception = null;
        }
    }
}
