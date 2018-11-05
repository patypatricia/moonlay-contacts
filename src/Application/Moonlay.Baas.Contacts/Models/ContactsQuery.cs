using GraphQL.Types;
using Moonlay.Contacts.Application;

namespace Moonlay.Baas.Contacts.Models
{
    public class ContactsQuery : ObjectGraphType
    {
        public ContactsQuery(IContactService contatService)
        {
            Field<ContactType>(
                "contact",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context => contatService.GetAsync(context.GetArgument<int>("id")).Result
            );

            Field<ListGraphType<ContactType>>(
                "contacts",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "page" }, new QueryArgument<IntGraphType> { Name = "page_size" }),
                resolve: context => contatService.GetAllAsync(context.GetArgument<int>("page"), context.GetArgument<int>("page_size")).Result
            );
        }
    }
}