using Microsoft.EntityFrameworkCore;
using Moonlay.Contacts.Domain;
using Moonlay.Domain;

namespace Moonlay.Contacts.Data
{
    public interface IContactDbContext : IUnitOfWork
    {
        DbSet<Contact> Contacts { get; }
    }
}