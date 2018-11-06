using Core.Models.PARAM;
using Core.Models.Utils;
using Core.Services.PARAM;
using Core.Services.Utils;
using DAOs.Utils;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAOs.Utils
{
    public class EntitiesDAO : BaseDAO, IEntitiesService
    {
        public EntitiesDAO(IDapperAdapter dapper) : base(dapper)
        {
        }

        public Result<List<ColumnTable>> GetListColumnsTable(string schema, string table)
        {
            var result = new Result<List<ColumnTable>>();
            try
            {
                using (var connection = _dapperAdapter.Get())
                {
                    result.Data = result.Data = connection.Query<ColumnTable>(@"SELECT table_catalog, table_schema, table_name, column_name, is_nullable, data_type, character_maximum_length
                            FROM information_schema.columns where table_schema = @Schema and table_name = @Table  order by ordinal_position", new{ Schema = schema , Table = table}).AsList();

                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando columnas.";
                result.Exception = ex;
            }
            return result;
        }
    }
}
