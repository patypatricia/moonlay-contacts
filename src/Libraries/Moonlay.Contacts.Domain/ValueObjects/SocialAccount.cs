using Moonlay.Domain;
using System.Collections.Generic;

namespace Moonlay.Contacts.Domain.ValueObjects
{
    public sealed class SocialAccount : ValueObject
    {
        protected override IEnumerable<object> GetAtomicValues()
        {
            throw new System.NotImplementedException();
        }
    }
}