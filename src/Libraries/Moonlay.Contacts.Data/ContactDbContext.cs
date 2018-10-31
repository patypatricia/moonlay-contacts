using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Moonlay.Contacts.Domain;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Moonlay.Contacts.Data
{
    public class ContactDbContext : DbContext, IContactDbContext
    {
        private readonly IMediator _mediator;

        public ContactDbContext(DbContextOptions<ContactDbContext> options)
        : base(options)
        { }

        public ContactDbContext(DbContextOptions<ContactDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

            System.Diagnostics.Debug.WriteLine("ContactDbContext::ctor ->" + this.GetHashCode());
        }

        public DbSet<Contact> Contacts => this.Set<Contact>();

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            // Dispatch Domain Events collection. 
            // Choices:
            // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
            // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
            // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
            // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
            await _mediator.DispatchDomainEventsAsync(this);

            // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
            // performed through the DbContext will be committed
            var result = await base.SaveChangesAsync();

            return true;
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await _mediator.DispatchDomainEventsAsync(this);

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>(ConfigContact);
        }

        private void ConfigContact(EntityTypeBuilder<Contact> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.NamesJson).IsRequired();

        }
    }
}
