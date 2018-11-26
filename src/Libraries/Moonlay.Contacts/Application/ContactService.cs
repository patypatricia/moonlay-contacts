using Moonlay.Contacts.Domain;
using Moonlay.Contacts.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Moonlay.Contacts.Application
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _contactRepository;

        public ContactService(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public async Task<Contact> AddPeopleAsync(People people)
        {
            var contact = await _contactRepository.AddAsync(people);

            await _contactRepository.UnitOfWork.SaveChangesAsync();

            return contact;
        }

        public async Task<IEnumerable<Contact>> GetAllAsync(int page, int pageSize)
        {
            var query = await _contactRepository.GetAllAsync();
            return query.Skip(page * pageSize).Take(pageSize).ToList();
        }

        public async Task<Contact> GetAsync(Guid id)
        {
            return await _contactRepository.GetAsync(id);
        }
    }
}