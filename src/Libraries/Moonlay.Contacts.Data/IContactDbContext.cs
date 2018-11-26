using Microsoft.EntityFrameworkCore;
using Moonlay.Contacts.Domain;
using Moonlay.Contacts.Domain.ReadModels;
using Moonlay.Domain;

namespace Moonlay.Contacts.Infrastructure
{
    public interface IContactDbContext : IUnitOfWork
    {
        DbSet<ContactReadModel> Contacts { get; }
    }
}