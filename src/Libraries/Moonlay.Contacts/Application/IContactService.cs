using Moonlay.Contacts.Domain;
using Moonlay.Contacts.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Moonlay.Contacts.Application
{
    public interface IContactService
    {
        Task<Contact> CreateContactAsync(People people);

        Task<Contact> CreateContactAsync(Company company);
    }
}