using GraphQL.Types;
using Moonlay.Contacts.Application;
using Moonlay.Contacts.Domain.ValueObjects;
using System.Collections.Generic;

namespace Moonlay.Baas.Contacts.Models
{
    public struct PeopleForm
    {
        public string FirstName;
        public string LastName;
    }

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
                    var arg = context.GetArgument<PeopleForm>("people");

                    return contactService.CreateContactAsync(new People(arg.FirstName, arg.LastName));
                });
        }
    }
}