using Moonlay.Contacts.Data;
using Moonlay.Contacts.Domain;
using Moonlay.Contacts.Domain.ValueObjects;
using Moonlay.Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moonlay.Contacts.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly IContactDbContext _dbContext;

        public ContactRepository(IContactDbContext dbContext)
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
