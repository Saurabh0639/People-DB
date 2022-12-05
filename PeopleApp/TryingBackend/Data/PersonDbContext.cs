using Microsoft.EntityFrameworkCore;
using TryingBackend.Models;

namespace TryingBackend.Data
{
    public class PersonDbContext :DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options) : base(options)
        {

        }
        public DbSet<Person> Persons { get; set; }

    }
}
