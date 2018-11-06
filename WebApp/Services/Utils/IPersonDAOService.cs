using Core.Models.PARAM;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.Utils
{
    public interface IEntitiesService
    {
        Result<List<ColumnTable>> GetListColumnsTable(string schema, string table);
    }
}
