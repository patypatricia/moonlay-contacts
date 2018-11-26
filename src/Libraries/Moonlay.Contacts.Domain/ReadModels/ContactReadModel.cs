using Moonlay.Domain;
using System;

namespace Moonlay.Contacts.Domain.ReadModels
{
    public class ContactReadModel : Entity
    {
        public Guid Identity { get; set; }

        public string NamesJson { get; set; }

        public string AddressJson { get; set; }

        public string PhonesJson { get; set; }
    }
}
