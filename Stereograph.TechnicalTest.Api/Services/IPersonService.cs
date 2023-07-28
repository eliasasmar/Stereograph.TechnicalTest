using Stereograph.TechnicalTest.Api.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Stereograph.TechnicalTest.Api.Services
{
    public interface IPersonService
    {
        List<Person> GetPersons();
        Person GetSinglePerson(Guid id);
        Person AddPerson(Person person);
        Person UpdatePerson(Person person);
        String DeletePerson(Guid id);
        void AddRecordCSV(Person person);
    }
}
