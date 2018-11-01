using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moonlay.Contacts.Application;
using Moonlay.Contacts.Data;
using Moonlay.Contacts.Domain;
using Moonlay.Contacts.EventHandlers;
using Moonlay.Contacts.Repositories;
using Moonlay.Domain;

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

            services.AddTransient<IContactDbContext>(c => c.GetService<ContactDbContext>());
            services.AddTransient<IUnitOfWork>(c => c.GetService<ContactDbContext>());

            services.AddTransient<IContactService, ContactService>();
            services.AddTransient<IContactRepository, ContactRepository>();
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
            app.UseMvc();
        }
    }
}
