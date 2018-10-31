using MediatR;
using Moonlay.Contacts.Domain;
using Moonlay.Contacts.Domain.Events;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moonlay.Contacts.EventHandlers
{
    public sealed class ContactCreatedHandler : INotificationHandler<ContactCreated>
    {
        private readonly IContactRepository _contactRepository;

        public ContactCreatedHandler(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public Task Handle(ContactCreated notification, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}
