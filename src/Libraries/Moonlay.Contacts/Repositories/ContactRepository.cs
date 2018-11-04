using Moonlay.Contacts.Data;
using Moonlay.Contacts.Domain;
using Moonlay.Contacts.Domain.ValueObjects;
using Moonlay.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moonlay.Contacts.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly ContactDbContext _dbContext;

        public ContactRepository(ContactDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUnitOfWork UnitOfWork => _dbContext;

        public Task<Contact> AddAsync(People people)
        {
            var contact = new Contact(Guid.NewGuid(), new List<string> { people.FirstName, people.LastName }, people.Addresses, people.Phones);

            contact.NamesJson = JsonConvert.SerializeObject(contact.Names);
            contact.AddressJson = JsonConvert.SerializeObject(contact.Addresses);
            contact.PhonesJson = JsonConvert.SerializeObject(contact.Phones);

            _dbContext.Contacts.Add(contact);

            return Task.FromResult(contact);
        }

        public Task<IEnumerable<Contact>> GetAllAsync(int page, int pageSize)
        {
            return Task.FromResult(_dbContext.Contacts.OrderBy(o => o.Names)
                .Skip(page * pageSize)
                .Take(pageSize)
                .AsEnumerable());
        }

        public Task<Contact> GetAsync(int contactId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}