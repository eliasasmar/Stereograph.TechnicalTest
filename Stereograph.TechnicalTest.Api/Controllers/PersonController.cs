using Microsoft.AspNetCore.Mvc;
using System;
using CsvHelper;
using System.IO;
using System.Globalization;
using System.Linq;
using Stereograph.TechnicalTest.Api.Models;
using Stereograph.TechnicalTest.Api.Migrations;
using Microsoft.Extensions.Options;

namespace Stereograph.TechnicalTest.Api.Controllers;

//[Route("api/persons")]
[ApiController]
public class PersonController : ControllerBase
{
    private ApplicationDbContext _context;

    public PersonController(ApplicationDbContext context)
    {
        _context = context;
    }
    [HttpGet]
    [Route("persons/GeneratePersons")]
    public string GeneratePersons()
    {
        try
        {
            int counter = 0;
            using (var streamReader = new StreamReader(@"C:\Users\Elias\Desktop\MyProjects\technique\TechnicalTestNetSkeleton\Stereograph.TechnicalTest.Api\Ressources\Persons.csv"))
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
                            counter++;
                        }catch (Exception ex)
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
