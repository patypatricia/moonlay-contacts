using Moonlay.Contacts.Domain;
using Moonlay.Contacts.Domain.ValueObjects;
using Moonlay.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moonlay.Contacts.Application
{
    public interface IContactService
    {
        Task<Contact> AddPeopleAsync(People people);
        Task<IEnumerable<Contact>> FindAllAsync(int page, int pageSize);
    }
}
