using Moonlay.Contacts.Domain;
using Moonlay.Contacts.Domain.ValueObjects;
using Moonlay.Domain;
using System;
using System.Collections.Generic;
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

        public Task<IEnumerable<Contact>> FindAllAsync(int page, int pageSize)
        {
            return Task.FromResult(_contactRepository.GetAllAsync(page, pageSize));
        }
    }
}
