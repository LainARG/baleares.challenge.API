using baleares.challenge.API.Infrastructure.Repository.Context;
using baleares.challenge.API.infrastructure.repository.interfaces;
using baleares.challenge.API.model.contacts;
using Microsoft.EntityFrameworkCore;

namespace baleares.challenge.API.infrastructure.repository;

    public class ContactRepository : IContactRepository
    {
        private readonly BalearesContext _context;

        public ContactRepository(BalearesContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Contact>> GetAllAsync() =>
            await _context.Contact.ToListAsync();

        public async Task<Contact?> GetByIdAsync(int id) =>
            await _context.Contact.FindAsync(id);

        public async Task AddAsync(Contact contact)
        {
            _context.Contact.Add(contact);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Contact contact)
        {
            _context.Contact.Remove(contact);
            await _context.SaveChangesAsync();
        }

    public async Task<Contact?> Search(Contact contact)
    {
        return await _context.Contact
            .Where(c => (contact.Email != null && c.Email.Contains(contact.Email)))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Contact>> SearchAsync(string query) =>
            await _context.Contact.Where(c =>
                c.Email.Contains(query) ||
                c.Phone.Contains(query) ||
                c.PhoneWork.Contains(query)).ToListAsync();

    public async Task<IEnumerable<Contact>> GetByCityOrProvinceAsync(string query) =>
            await _context.Contact.Where(c =>
                c.Province.Contains(query) ||
                c.City.Contains(query)).ToListAsync();

    public async Task UpdateAsync(Contact contact)
    {
        _context.Contact.Update(contact); 
        await _context.SaveChangesAsync();
    }

}

