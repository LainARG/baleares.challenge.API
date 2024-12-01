using baleares.challenge.API.model.contacts;

namespace baleares.challenge.API.infrastructure.repository.interfaces;

    public interface IContactRepository
    {
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<Contact?> GetByIdAsync(int id);
        Task AddAsync(Contact contact);
        Task DeleteAsync(Contact contact);
        Task<Contact?> Search(Contact contact);
        Task<IEnumerable<Contact>> SearchAsync(string query);
        Task<IEnumerable<Contact>> GetByCityOrProvinceAsync(string query);
        Task UpdateAsync(Contact contact);
    }

