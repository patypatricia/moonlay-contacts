
using Azure.WebJobs.Extensions.DependencyInjection;
using MediatR;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using Microsoft.Azure.WebJobs.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moonlay.Contacts.Application;
using Moonlay.Contacts.Data;
using Moonlay.Contacts.Domain;
using Moonlay.Contacts.EventHandlers;
using Moonlay.Contacts.Repositories;
using Moonlay.Domain;
using Moonlay.Faas.Contacts;
using System;
using System.Reflection;

[assembly: WebJobsStartup(typeof(Startup))]
namespace Moonlay.Faas.Contacts
{
    internal class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder) =>
            builder.AddDependencyInjection<ServiceProviderBuilder>();
    }

    internal class ServiceProviderBuilder : IServiceProviderBuilder
    {
        private readonly IConfiguration _config;
        private readonly ILoggerFactory _loggerFactory;

        public ServiceProviderBuilder(ILoggerFactory loggerFactory)
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            _loggerFactory = loggerFactory;
        }

        public IServiceProvider Build()
        {
            var services = new ServiceCollection();

            services.AddSingleton(_ => _loggerFactory.CreateLogger(LogCategories.CreateFunctionUserCategory("Common")));

            services.AddMediatR(typeof(ContactCreatedHandler).GetTypeInfo().Assembly);

            services.AddDbContext<ContactDbContext>(options =>
            {
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
            })
            .AddSingleton<IContactDbContext>(c => c.GetRequiredService<ContactDbContext>())
            .AddSingleton<IUnitOfWork>(c => c.GetRequiredService<ContactDbContext>());

            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IContactRepository, ContactRepository>();

            return services.BuildServiceProvider();
        }
    }
}
