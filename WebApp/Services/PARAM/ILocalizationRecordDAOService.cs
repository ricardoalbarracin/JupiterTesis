using Core.Models.PARAM;
using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.PARAM
{
    public interface ILocalizationRecordDAOService
    {
        Result<List<LocalizationRecord>> GetListLocalizationRecords(bool refresh = false);

        Result<List<LocalizationRecordType>> GetListLocalizationRecordTypes();

        Result<List<LocalizationCulture>> GetListLocalizationCultures();

        Result<LocalizationRecord> GetLocalizationRecordById(long id);

        Result UpdLocalizationRecord(LocalizationRecord localizationRecord);

        Result<LocalizationRecord> InsLocalizationRecord(LocalizationRecord localizationRecord);
    }
}
