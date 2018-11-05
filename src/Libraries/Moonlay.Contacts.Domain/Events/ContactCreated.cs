using MediatR;

namespace Moonlay.Contacts.Domain.Events
{
    public class ContactCreated : INotification
    {
        public ContactCreated(Contact contact)
        {
            Data = contact;
        }

        public Contact Data { get; }
    }
}