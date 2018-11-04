using GraphQL;
using GraphQL.Types;

namespace Moonlay.Baas.Contacts.Models
{
    public class ContactsSchema : Schema
    {
        public ContactsSchema(IDependencyResolver resolver) : base(resolver)
        {
            Query = resolver.Resolve<ContactsQuery>();
            Mutation = resolver.Resolve<ContactsMutation>();
        }
    }
}