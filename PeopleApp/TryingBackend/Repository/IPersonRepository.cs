using TryingBackend.Data;
using TryingBackend.Models;

namespace TryingBackend.Repository
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> Getallpersons();
        Task<Person> Get(Guid id);
        Task<Person> CreatePerson(Person person);
        Task<Person> UpdatePerson(Person person);
        void DeletePerson(Guid id);
        bool SaveChanges();


    }
}
