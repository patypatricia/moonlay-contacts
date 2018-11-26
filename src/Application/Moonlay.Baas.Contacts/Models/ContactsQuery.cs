using GraphQL.Types;
using Moonlay.Contacts.Application;
using System;

namespace Moonlay.Baas.Contacts.Models
{
    public class ContactsQuery : ObjectGraphType
    {
        public ContactsQuery(IContactService contatService)
        {
            Field<ContactType>(
                "contact",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "id" }),
                resolve: context => contatService.GetAsync(Guid.Parse(context.GetArgument<string>("id"))).Result
            );

            Field<ListGraphType<ContactType>>(
                "contacts",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "page" }, new QueryArgument<IntGraphType> { Name = "page_size" }),
                resolve: context => contatService.GetAllAsync(context.GetArgument<int>("page"), context.GetArgument<int>("page_size")).Result
            );
        }
    }
}