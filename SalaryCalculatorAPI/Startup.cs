﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SalaryCalculator.Business;
using SalaryCalculator.DAL.Repositories;
using SalaryCalculator.Entities.Repositories;
using Swashbuckle.AspNetCore.Swagger;

namespace SalaryCalculatorAPI
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            
            services.AddScoped<ISalarySupervisor, SalarySupervisor>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.ConfigureSwaggerGen(options =>
            {
                options.IncludeXmlComments(Path.ChangeExtension(Assembly.GetEntryAssembly().Location, "xml"));
                //options.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Endava Talents API",
                    Description = "API Endpoint For Endava Talents System",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "LeonardoSanchez",
                        Email = "leonardo.sanchez@endava.com",
                        Url = "https://twitter.com/ingleo44"
                    }
                });

            });

            services.AddCors(o => o.AddPolicy("ETPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Endava Talents API V1");
                c.RoutePrefix = string.Empty;
            });

            app.UseCors("ETPolicy");
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
