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
using Microsoft.Extensions.Caching.Distributed;
using Dapper.Contrib.Extensions;
using Newtonsoft.Json;

namespace DAOs
{
    public class DapperAdapter : IDapperAdapter
    {
        private string _connectionString;
        private IConfigurationRoot _configuration;

        public DapperAdapter(IConfiguration configuration)
        {
            _configuration = configuration as IConfigurationRoot;

            _connectionString = _configuration.GetConnectionString("DbDefaultConnection");
        }

        public IDbConnection Get()
        {
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            return new NpgsqlConnection(_connectionString);
        }

    }

    public static class Dappertensions
    {
        public static IEnumerable<T> GetAll<T>(this IDbConnection cnn, IDistributedCache distributedCache, bool refresh= false) where T : class
        {
            var key = $"GET_ALL_{typeof(T).FullName.ToUpper()}";
            var cacheLocalizationRecords = distributedCache.GetString(key);
            if (cacheLocalizationRecords == null || refresh)
            {
                var data = cnn.GetAll<T>();
                distributedCache.SetString(key, JsonConvert.SerializeObject(data));
                return data;
            }
            else
            {
                return JsonConvert.DeserializeObject<List<T>>(cacheLocalizationRecords);
            }
        }

        public static IEnumerable<T> Query<T>(this IDbConnection cnn, string sql, IDistributedCache distributedCache, bool refresh = false) where T : class
        {
            var key = $"Query_{typeof(T).FullName.ToUpper()}_{sql.Replace(" ", "").ToUpper()}";
            var cacheLocalizationRecords = distributedCache.GetString(key);
            if (cacheLocalizationRecords == null || refresh)
            {
                var data = cnn.GetAll<T>();
                distributedCache.SetString(key, JsonConvert.SerializeObject(data));
                return data;
            }
            else
            {
                return JsonConvert.DeserializeObject<List<T>>(cacheLocalizationRecords);
            }
        }
    }
}
