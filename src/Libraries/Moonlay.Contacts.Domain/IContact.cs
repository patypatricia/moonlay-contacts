using Moonlay.Contacts.Domain.ValueObjects;
using System;

namespace Moonlay.Contacts.Domain
{
    public interface IContact
    {
        void AddAddress(Address address);
        void AddPhone(Phone phone);
    }
}
