using Core.Models.PARAM;
using Core.Models.SEG;
using Core.Models.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Services.PARAM
{
    public interface IPersonDAOService
    {
        Result<List<Person>> GetListPersons();

        Result<Person> GetPersonById(long id);

        Result UpdPerson(Person person);

        Result<Person> InsPerson(Person person);
    }
}
