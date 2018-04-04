using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Core.Services.Utils;

namespace DAOs
{
    public class DapperAdapter: IDapperAdapter
    {
        private string _connectionString;
        private IConfigurationRoot _configuration;

        public DapperAdapter(IConfiguration configuration) 
        {
            _configuration = configuration as IConfigurationRoot;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            _connectionString = _configuration.GetConnectionString("LocalConnection");
        }

        public IDbConnection Open()
        {
            return new SqlConnection(_connectionString);
        }
        
    }
}
