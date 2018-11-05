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
using Npgsql;

namespace DAOs
{
    public class DapperAdapter: IDapperAdapter
    {
        private string _connectionString;
        private IConfigurationRoot _configuration;

        public DapperAdapter(IConfiguration configuration) 
        {
            _configuration = configuration as IConfigurationRoot;
            
            _connectionString = _configuration.GetConnectionString("DbDefaultConnection");
        }

        public IDbConnection Open()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            return new NpgsqlConnection(_connectionString);
        }
        
    }
}
