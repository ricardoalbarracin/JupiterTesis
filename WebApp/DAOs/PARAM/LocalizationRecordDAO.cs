using Core.Models.PARAM;
using Core.Models.Utils;
using Core.Services.PARAM;
using Core.Services.Utils;
using DAOs.Utils;
using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAOs.PARAM
{
    public class LocalizationRecordDAO : BaseDAO, ILocalizationRecordDAOService
    {
        public LocalizationRecordDAO(IDapperAdapter dapper, IDistributedCache distributedCache) : base(dapper, distributedCache)
        {
        }
        public Result<List<LocalizationRecord>> GetListLocalizationRecords(bool refresh = false)
        {
            var result = new Result<List<LocalizationRecord>>();
            try
            {
                using (var connection = _dapperAdapter.Get())
                {
                    result.Data = connection.GetAll<LocalizationRecord>(_distributedCache, refresh).ToList();
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando localizationRecordas.";
                result.Exception = ex;
            }
            return result;
        }



        public Result<LocalizationRecord> GetLocalizationRecordById(long id)
        {
            var result = new Result<LocalizationRecord>();
            try
            {
                using (var connection = _dapperAdapter.Get())
                {
                    result.Data = connection.Get<LocalizationRecord>(id);
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando localizationRecorda.";
                result.Exception = ex;
            }
            return result;
        }

        public Result UpdLocalizationRecord(LocalizationRecord localizationRecord)
        {
            var result = new Result();
            try
            {
                using (var connection = _dapperAdapter.Get())
                {
                    connection.Execute(@"UPDATE param.localization_record 
                                        SET localization_cluture_id = @LocalizationClutureId, 
                                            code = @Code,
                                            description = @Description, 
                                            type_id = @TypeId
                                         WHERE id = @Id;", localizationRecord);
                    result.Message = "LocalizationRecord actualizada correctamente.";
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error actualizando localizationRecorda.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<LocalizationRecord> InsLocalizationRecord(LocalizationRecord localizationRecord)
        {
            var result = new Result<LocalizationRecord>();
            try
            {
                using (var connection = _dapperAdapter.Get())
                {
                    localizationRecord.Id = connection.QuerySingle<int>(@"INSERT INTO param.localization_record
                                                            ( localization_cluture_id, code, description, type_id)
                                                             VALUES ( @LocalizationClutureId, @Code, @Description, @TypeId )
                                                            returning id;", localizationRecord);
                     
                    result.Message = "LocalizationRecord creada correctamente.";
                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error creando localizationRecorda.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<List<LocalizationRecordType>> GetListLocalizationRecordTypes()
        {
            var result = new Result<List<LocalizationRecordType>>();
            try
            {
                using (var connection = _dapperAdapter.Get())
                {
                    result.Data = connection.GetAll<LocalizationRecordType>(_distributedCache).ToList();

                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando LocalizationRecordType.";
                result.Exception = ex;
            }
            return result;
        }

        public Result<List<LocalizationCulture>> GetListLocalizationCultures()
        {
            var result = new Result<List<LocalizationCulture>>();
            try
            {
                using (var connection = _dapperAdapter.Get())
                {
                    result.Data = connection.GetAll<LocalizationCulture>(_distributedCache).ToList();

                    result.Success = true;
                }
            }
            catch (Exception ex)
            {
                result.Message = "Error consultando LocalizationCulture.";
                result.Exception = ex;
            }
            return result;
        }
    }
}
