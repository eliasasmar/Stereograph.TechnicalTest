using Microsoft.AspNetCore.Mvc;
using System;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using Stereograph.TechnicalTest.Api.Models;
using Stereograph.TechnicalTest.Api.Migrations;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Stereograph.TechnicalTest.Api.Services;

namespace Stereograph.TechnicalTest.Api.Controllers;

//[Route("api/persons")]
[ApiController]
public class PersonController : ControllerBase
{
    private IConfiguration _configuration;
    private IPersonService personService;

    public PersonController(IConfiguration configuration, IPersonService _personService)
    {
        _configuration = configuration;
        personService = _personService;
        
    }
    [HttpGet]
    [Route("persons/GeneratePersons")]
    public string GeneratePersons()
    {
        try
        {

            string CSVPath = _configuration.GetValue<string>("MyConfig:PathCSV");
            using (var streamReader = new StreamReader(CSVPath))
            {
                using (var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {
                    csvReader.Context.RegisterClassMap<PersonClassMap>();
                    var records = csvReader.GetRecords<Person>().ToList();

                    foreach (var record in records)
                    {
                        try
                        {
                            personService.AddRecordCSV(record);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }

                    }
                }
            }
            return "Persons Created";
        }catch(Exception ex)
        {
            return "Error";
        }
    }

    [HttpPost]
    [Route("persons/AddPerson")]
    public ActionResult<Person> AddPerson(Person person)
    {
            personService.AddPerson(person);
            return Ok(person);

    }

    [HttpGet]
    [Route("persons/GetPersons")]
    public ActionResult<List<Person>> GetPersons()
    {
        return Ok(personService.GetPersons());
    }

    [HttpGet]
    [Route("persons/GetSinglePerson/{id}")]
    public ActionResult<Person> GetSinglePerson(Guid id)
    {
        var person = personService.GetSinglePerson(id);
        if (person == null)
        {
            return BadRequest("Person not found.");
        }
        return Ok(person);
    }

    [HttpPost]
    [Route("persons/UpdatePerson")]
    public ActionResult<Person> UpdatePerson(Person person)
    {
        try
        {
            var result = personService.UpdatePerson(person);
            if (result == null)
            {
                return BadRequest("Person not found");
            }
            else
            {
                return Ok(person);
            }
            
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }

    }

    [HttpGet]
    [Route("persons/DeletePerson/{id}")]
    public ActionResult<String> DeletePerson(Guid id)
    {
        try
        {
            var result = personService.DeletePerson(id);
            if (result == null)
            {
                return BadRequest("Person not found");
            }
            else
            {
                return Ok(result);
            }

        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }

    }

}
