using Microsoft.Extensions.Configuration;
using Stereograph.TechnicalTest.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stereograph.TechnicalTest.Api.Services
{
    public class PersonService : IPersonService
    {
        private ApplicationDbContext _context;
        public PersonService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Person> GetPersons()
        {
            return _context.Persons.ToList();
        }

        public Person GetSinglePerson(Guid id)
        {
            var person = _context.Persons.Where(person => person.personId == id);
            if(person == null)
            {
                return null;
            }
            else
            {
                return person.FirstOrDefault();
            }
            
        }
        public Person AddPerson(Person person)
        {
            person.personId = Guid.NewGuid();
            _context.Persons.Add(person);
            _context.SaveChangesAsync();
            return person;
        }

        public Person UpdatePerson(Person person)
        {
            var personExist = _context.Persons.Where(x => x.personId == person.personId).FirstOrDefault();
            if (personExist == null)
            {
                return null;
            }
            else
            {
                personExist.firstName = person.firstName;
                personExist.lastName = person.lastName;
                personExist.email = person.email;
                personExist.address = person.address;
                personExist.city = person.city;
                _context.SaveChanges();

                return person;
            }
        }

        public String DeletePerson(Guid id)
        {
            var personExist = _context.Persons.SingleOrDefault(x => x.personId == id);
            if (personExist == null)
            {
                return null;
            }
            else
            {
                _context.Remove(personExist);
                _context.SaveChanges();

                return $"Person with the Id {id} is deleted";
            }
        }

        public void AddRecordCSV(Person person)
        {
            _context.Add(person);
            _context.SaveChanges();
        }
    }
}
