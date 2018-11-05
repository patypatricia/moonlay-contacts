using Moonlay.Domain;
using System.Collections.Generic;

namespace Moonlay.Contacts.Domain.ValueObjects
{
    public sealed class Phone : ValueObject
    {
        public Phone(string name, string number)
        {
            Name = name;
            Number = number;
        }

        public string Name { get; }
        public string Number { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return Number;
        }
    }
}