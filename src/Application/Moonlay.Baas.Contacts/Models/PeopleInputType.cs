using GraphQL.Types;

namespace Moonlay.Baas.Contacts.Models
{
    public class PeopleInputType : InputObjectGraphType
    {
        public PeopleInputType()
        {
            Name = "PeopleInput";

            Field<NonNullGraphType<StringGraphType>>("firstName");
            Field<NonNullGraphType<StringGraphType>>("lastName");
        }
    }
}