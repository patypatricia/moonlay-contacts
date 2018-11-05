using GraphQL.Types;
using Moonlay.Contacts.Domain;

namespace Moonlay.Baas.Contacts.Models
{
    public class ContactType : ObjectGraphType<Contact>
    {
        public ContactType()
        {
            Field(x => x.Id);

            Field<StringGraphType>("identity", resolve: context => context.Source.Identity.ToString());

            Field<StringGraphType>("names", resolve: context => string.Join(" ", context.Source.Names));
        }
    }
}