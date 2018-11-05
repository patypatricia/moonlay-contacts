using Moonlay.Contacts.Domain;
using Moonlay.Contacts.Domain.ValueObjects;
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

        public async Task<IEnumerable<Contact>> GetAllAsync(int page, int pageSize)
        {
            return await _contactRepository.GetAllAsync(page, pageSize);
        }

        public async Task<Contact> GetAsync(int id)
        {
            return await _contactRepository.GetAsync(id);
        }
    }
}