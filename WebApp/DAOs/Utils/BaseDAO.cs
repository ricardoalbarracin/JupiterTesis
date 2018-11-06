using Core.Services.Utils;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAOs.Utils
{
    public class BaseDAO
    {
        protected IDapperAdapter _dapperAdapter;
        protected IDistributedCache _distributedCache;
        protected BaseDAO(IDapperAdapter dapper)
        {
            _dapperAdapter = dapper;
        }

        protected BaseDAO(IDapperAdapter dapper, IDistributedCache distributedCache)
        {
            _dapperAdapter = dapper;
            _distributedCache = distributedCache;
        }
    }
}
