using GraphQL.Types;
using Moonlay.Contacts.Application;
using Moonlay.Contacts.Domain.ValueObjects;

namespace Moonlay.Baas.Contacts.Models
{
    public class ContactsMutation : ObjectGraphType
    {
        public ContactsMutation(IContactService contactService)
        {
            Field<ContactType>(
                "newPeople",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<PeopleInputType>> { Name = "people" }
                ),
                resolve: context =>
                {
                    var people = context.GetArgument<People>("people");

                    return contactService.AddPeopleAsync(people);
                });
        }
    }
}