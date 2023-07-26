using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using System;

namespace Stereograph.TechnicalTest.Api.Models
{
    public class PersonClassMap : ClassMap<Person>
    {
        public PersonClassMap() 
        {
            Map(m => m.personId).Convert(row => Guid.NewGuid());
            Map(m => m.firstName).Name("first_name");
            Map(m => m.lastName).Name("last_name");
            Map(m => m.email).Name("email");
            Map(m => m.address).Name("address");
            Map(m => m.city).Name("city");
        }
    }
}
