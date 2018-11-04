using Moonlay.Domain;
using System.Collections.Generic;

namespace Moonlay.Contacts.Domain.ValueObjects
{
    public sealed class Address : ValueObject
    {
        public string Name { get; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string Country { get; private set; }
        public string ZipCode { get; private set; }

        private Address()
        {
        }

        public Address(string name, string street, string city, string state, string country, string zipcode)
        {
            Name = name;
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipcode;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Name;
            yield return Street;
            yield return City;
            yield return State;
            yield return Country;
            yield return ZipCode;
        }
    }
}