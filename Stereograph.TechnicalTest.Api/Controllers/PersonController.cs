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

namespace Stereograph.TechnicalTest.Api.Controllers;

//[Route("api/persons")]
[ApiController]
public class PersonController : ControllerBase
{
    private ApplicationDbContext _context;
    private IConfiguration _configuration;

    public PersonController(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
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
                            _context.Add(record);
                            _context.SaveChanges();
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
    public async Task<ActionResult<List<Person>>> AddPerson(Person person)
    {
        try
        {
            person.personId = Guid.NewGuid();
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return Ok(await _context.Persons.ToListAsync());
        }catch(Exception ex)
        {
            return BadRequest(ex.ToString());
        }

    }

    [HttpGet]
    [Route("persons/GetPersons")]
    public async Task<ActionResult<List<Person>>> GetPersons()
    {
        return Ok(await _context.Persons.ToListAsync());
    }

    [HttpGet]
    [Route("persons/GetSinglePerson/{id}")]
    public ActionResult<Person> GetSinglePerson(Guid id)
    {
        var person = _context.Persons.Where(person => person.personId == id).FirstOrDefault();
        if (person == null)
        {
            return BadRequest("Person not found.");
        }
        return Ok(person);
    }

}
