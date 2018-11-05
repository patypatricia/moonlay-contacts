using Moonlay.Contacts.Domain.ValueObjects;

namespace Moonlay.Contacts.Domain
{
    public interface IContact
    {
        void AddAddress(Address address);

        void AddPhone(Phone phone);
    }
}