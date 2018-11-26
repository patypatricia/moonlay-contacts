using Moonlay.Contacts.Domain;
using Moonlay.Contacts.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moonlay.Contacts.Application
{
    public interface IContactService
    {
        Task<Contact> AddPeopleAsync(People people);

        Task<IEnumerable<Contact>> GetAllAsync(int page, int pageSize);

        Task<Contact> GetAsync(Guid id);
    }
}