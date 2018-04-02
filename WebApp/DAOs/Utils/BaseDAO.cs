using Core.Services.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAOs.Utils
{
    public class BaseDAO
    {
        protected IDapperAdapter _dapperAdapter;
        protected BaseDAO(IDapperAdapter dapper)
        {
            _dapperAdapter = dapper;
        }
    }
}
