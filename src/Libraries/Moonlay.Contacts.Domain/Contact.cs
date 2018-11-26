using Moonlay.Contacts.Domain.Events;
using Moonlay.Contacts.Domain.ValueObjects;
using Moonlay.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Moonlay.Contacts.Domain
{
    public sealed class Contact : Entity, IAggregateRoot, IContact
    {
        public IReadOnlyList<string> Names { get; }

        public IReadOnlyList<Address> Addresses { get; private set; }

        public IReadOnlyList<Phone> Phones { get; private set; }

        public Guid Identity { get; set; }

        private Contact()
        {
        }

        public void SetId(int id) => Id = id;

        public Contact(Guid identity, List<string> names, List<Address> addresses, List<Phone> phones)
        {
            Identity = identity;
            Names = names;
            Addresses = addresses;
            Phones = phones;

            this.AddDomainEvent(new ContactCreated(this));
        }

        public void AddAddress(Address address)
        {
            var list = Addresses.ToList();

            if (!list.Any(o => o.Equals(address)))
                list.Add(address);
            else
                throw Validator.ErrorValidation(("Address", "Was Exists"));

            Addresses = list;
        }

        public void AddPhone(Phone phone)
        {
            var list = Phones.ToList();

            if (!list.Any(o => o.Equals(phone)))
                list.Add(phone);
            else
                throw Validator.ErrorValidation(("Phone", "Was Exists"));

            Phones = list;
        }
    }
}