using GraphiQl;
using GraphQL;
using GraphQL.Types;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moonlay.Baas.Contacts.Models;
using Moonlay.Contacts.Application;
using Moonlay.Contacts.Infrastructure;
using Moonlay.Contacts.Domain;
using Moonlay.Contacts.EventHandlers;
using Moonlay.Contacts.Repositories;
using Moonlay.Domain;
using System.Reflection;

namespace Moonlay.Baas.Contacts
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMediatR(typeof(ContactCreatedHandler).GetTypeInfo().Assembly);

            services.AddDbContext<ContactDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), sqlopt =>
                {
                    sqlopt.MigrationsAssembly(typeof(Startup).Assembly.FullName);
                });
            });

            services.AddTransient<IContactDbContext>(c => c.GetRequiredService<ContactDbContext>());
            services.AddTransient<IUnitOfWork>(c => c.GetRequiredService<ContactDbContext>());

            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IContactRepository, ContactRepository>();

            services.AddTransient<IDocumentExecuter, DocumentExecuter>();
            services.AddTransient<ContactsQuery>();
            services.AddTransient<ContactsMutation>();
            services.AddTransient<ContactType>();
            services.AddTransient<PeopleInputType>();

            var sp = services.BuildServiceProvider();
            services.AddSingleton<ISchema>(new ContactsSchema(new FuncDependencyResolver(type => sp.GetService(type))));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseGraphiQl("/graphql", "/api/graphql");
            app.UseMvc();
        }
    }
}