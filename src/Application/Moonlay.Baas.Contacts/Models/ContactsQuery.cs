using GraphQL.Types;
using Moonlay.Contacts.Application;
using Moonlay.Contacts.Domain;
using System;
using System.Linq;

namespace Moonlay.Baas.Contacts.Models
{
    public class ContactsQuery : ObjectGraphType
    {
        public ContactsQuery(IContactRepository repo)
        {
            Field<ContactType>(
                "contact",
                arguments: new QueryArguments(new QueryArgument<StringGraphType> { Name = "id" }),

                resolve: context => repo.GetAsync(Guid.Parse(context.GetArgument<string>("id"))).Result
            );

            Field<ListGraphType<ContactType>>(
                "contacts",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "page" }, new QueryArgument<IntGraphType> { Name = "page_size" }),
                resolve: context => {
                    int page = context.GetArgument<int>("page"), pageSize = context.GetArgument<int>("page_size");

                    var query = repo.GetAllAsync().Result;

                    return query.Skip(page * pageSize).Take(pageSize).ToList();
                }
            );
        }
    }
}