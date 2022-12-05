using Microsoft.EntityFrameworkCore;
using TryingBackend.Data;
using TryingBackend.Models;

namespace TryingBackend.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PersonDbContext _context;

        public PersonRepository(PersonDbContext context)
        {
            _context = context;
        }

        public async Task<Person> CreatePerson(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();
            return person;
        }

        public void DeletePerson(Guid id)
        {

            var idfind = _context.Persons.Find(id);
            if (idfind != null)
            {
                _context.Persons.Remove(idfind);
                _context.SaveChanges();

            }
        }

        public async Task<Person> Get(Guid id)
        {
            return await _context.Persons.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Person>> Getallpersons()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<Person> UpdatePerson(Person person)
        {

            _context.Persons.Update(person);
            await _context.SaveChangesAsync();
            return person;

        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }


    }
}
