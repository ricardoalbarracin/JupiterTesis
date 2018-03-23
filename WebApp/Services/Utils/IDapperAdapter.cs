using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Core.Services.Utils
{
    public interface IDapperAdapter
    {
        IDbConnection Open();
    }
}
