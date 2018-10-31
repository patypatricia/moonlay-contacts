using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Moonlay.Contacts.Data
{
    public class DesignTimeContactDbContext : IDesignTimeDbContextFactory<ContactDbContext>
    {
        public ContactDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<ContactDbContext> builder = new DbContextOptionsBuilder<ContactDbContext>();

            var context = new ContactDbContext(
                builder
                .UseSqlServer("Server=(LocalDb)\\MSSQLLocalDB;Database=MoonlayEWork.Dev;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options);

            return context;
        }
    }
}
