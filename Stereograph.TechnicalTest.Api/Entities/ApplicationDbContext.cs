using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;

namespace Stereograph.TechnicalTest.Api.Models;

public class ApplicationDbContext : DbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    public DbSet<Person> Persons { get; set; }
}
