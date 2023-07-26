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
}
