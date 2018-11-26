using Moonlay.Contacts.Domain;
using Moonlay.Contacts.Domain.ReadModels;
using Moonlay.Contacts.Domain.ValueObjects;
using Moonlay.Contacts.Infrastructure;
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



        public Task<Contact> AddAsync(Contact contact)
        {
            var readModel = new ContactReadModel { };

            readModel.NamesJson = JsonConvert.SerializeObject(contact.Names);
            readModel.AddressJson = JsonConvert.SerializeObject(contact.Addresses);
            readModel.PhonesJson = JsonConvert.SerializeObject(contact.Phones);

            contact.DomainEvents.ToList().ForEach(o => readModel.AddDomainEvent(o));

            _dbContext.Contacts.Add(readModel);

            contact.ClearDomainEvents();

            return Task.FromResult(contact);
        }

        public async Task<IQueryable<Contact>> GetAllAsync()
        {
            await Task.Yield();

            return _dbContext.Contacts.Select(o => new Contact(o.Identity,
                JsonConvert.DeserializeObject<List<string>>(o.NamesJson),
                JsonConvert.DeserializeObject<List<Address>>(o.AddressJson),
                JsonConvert.DeserializeObject<List<Phone>>(o.PhonesJson)));

        }

        public async Task<Contact> GetAsync(Guid id)
        {
            var query = await this.GetAllAsync();
            return query.Where(o => o.Identity == id).FirstOrDefault();
        }

        public async Task UpdateAsync(Contact contact)
        {
            var readModel = _dbContext.Contacts.FirstOrDefault(o => o.Identity == contact.Identity);

            readModel.NamesJson = JsonConvert.SerializeObject(contact.Names);
            readModel.AddressJson = JsonConvert.SerializeObject(contact.Addresses);
            readModel.PhonesJson = JsonConvert.SerializeObject(contact.Phones);

            contact.DomainEvents.ToList().ForEach(o => readModel.AddDomainEvent(o));

            _dbContext.Contacts.Update(readModel);

            contact.ClearDomainEvents();

            await Task.Yield();
        }
    }
}