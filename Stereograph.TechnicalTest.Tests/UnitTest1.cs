using Castle.Core.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Stereograph.TechnicalTest.Api.Controllers;
using Stereograph.TechnicalTest.Api.Models;
using Stereograph.TechnicalTest.Api.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Stereograph.TechnicalTest.Tests;

public class UnitTest1
{
    private readonly Mock<IPersonService> personService;
    public UnitTest1() {
        personService = new Mock<IPersonService>();
        
    }
    [Fact]
    public void GetPersons_Test()
    {
        //var personList = GetPersonsData();
        //personService.Setup(x => x.GetPersons())
        //        .Returns(personList);
    }

    [Fact]
    public void GetSinglePerson_Test()
    {
        //var personList = GetPersonsData();
        //personService.Setup(x => x.GetSinglePerson(new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa7")))
        //    .Returns(personList[1]);
    }
    [Fact]
    public void AddPerson_Test()
    {
        var personList = GetPersonsData();
        //personService.Setup(x => x.AddPerson(personList[2]))
        //  .Returns(personList[2]);
        //var personController = new PersonController(_context, null,personService);
        personService.Setup(x => x.AddPerson(personList[2])).Returns(personList[2]).Verifiable();
        var actual = personService.Object.AddPerson(personList[2]);
        Assert.Equal(personList[2], actual);
        personService.Verify();
    }

    private List<Person> GetPersonsData()
    {
        List<Person> personsData = new List<Person>
        {
            new Person
            {
                personId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                firstName = "Linette",
                lastName = "Naylor",
                email = "lnaylorr8@opensource.org",
                address = "100 Macpherson Drive",
                city = "Hujirt"
            },
             new Person
            {
                personId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa7"),
                firstName = "Linette1",
                lastName = "Naylor1",
                email = "lnaylorr81@opensource.org",
                address = "100 Macpherson Drive1",
                city = "Hujirt1"
            },
             new Person
            {
                personId = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa8"),
                firstName = "Linette2",
                lastName = "Naylor2",
                email = "lnaylorr82@opensource.org",
                address = "100 Macpherson Drive2",
                city = "Hujirt"
            },
        };
        return personsData;
    }
}