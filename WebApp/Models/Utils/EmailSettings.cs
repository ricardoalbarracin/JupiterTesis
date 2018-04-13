using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Models.Utils
{
    public class EmailSettings
    {
        public String PrimaryDomain { get; set; }
        public int PrimaryPort { get; set; }
        public String UsernameEmail { get; set; }
        public String UsernamePassword { get; set; }
    }
}
