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

        public async Task<Contact> CreateContactAsync(Company company)
        {
           var contact = await _contactRepository.AddAsync(new Contact(Guid.NewGuid(), new List<string> { company.Name }, company.Addresses, company.Phones));

            await _contactRepository.UnitOfWork.SaveChangesAsync();

            return contact;
        }

        public async Task<Contact> CreateContactAsync(People people)
        {
            var contact = await _contactRepository.AddAsync(new Contact(Guid.NewGuid(), new List<string> { people.FirstName, people.LastName }, people.Addresses, people.Phones));

            await _contactRepository.UnitOfWork.SaveChangesAsync();

            return contact;
        }
    }
}