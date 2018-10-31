using Moonlay.Contacts.Domain;
using Moonlay.Contacts.Domain.ValueObjects;
using Moonlay.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moonlay.Contacts.Application
{
    public interface IContactService
    {
        Task<GenericResponse<Contact>> AddPeopleAsync(People people);
        Task<GenericResponse<IEnumerable<Contact>>> FindAllAsync(int page, int pageSize);
    }
}
