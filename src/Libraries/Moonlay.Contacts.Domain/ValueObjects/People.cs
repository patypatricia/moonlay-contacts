using Moonlay.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Moonlay.Contacts.Domain.ValueObjects
{
    public sealed class People : ValueObject, IPeople
    {
        private List<Phone> _phones;
        private List<Address> _address;

        public People(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public string FirstName { get; }
        public string LastName { get; }

        public List<Address> Addresses { get => _address; }
        public List<Phone> Phones { get => _phones; }

        public void AddAddress(Address address)
        {
            _address = _address ?? new List<Address>();

            if (!_address.Any(o => o.Equals(address)))
                _address.Add(address);
            else
                throw Validator.ErrorValidation(("Address", "Was Exists"));
        }

        public void AddPhone(Phone phone)
        {
            _phones = _phones ?? new List<Phone>();

            if (!_phones.Any(o => o.Equals(phone)))
                _phones.Add(phone);
            else
                throw Validator.ErrorValidation(("Phone", "Was Exists"));
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return FirstName;
            yield return LastName;
        }
    }
}
