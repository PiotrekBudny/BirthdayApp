﻿using BirthdayApi.CsvParser;
using BirthdayApi.Providers;
using BirthdayApi.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BirthdayApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IConfigurationWrapper, ConfigurationWrapper>();
            services.AddSingleton<IGetBirthdayPeopleDetailsResponseProvider, GetBirthdayPeopleDetailsResponseProvider>();
            services.AddSingleton<IAddBirthdayToListResponseProvider, AddBirthdayToListResponseProvider>();
            services.AddSingleton<IAddBirthdayToTheListHelper, AddBirthdayToTheListHelper>();
            services.AddSingleton<ICsvReaderWrapper, CsvReaderWrapper>();
            services.AddSingleton<ICsvWriterWrapper, CsvWriterWrapper>();
            services.AddSingleton<IBirthdayValidator, BirthdayValidator>();

            services.AddMvc(options =>
            {
                options.Filters.Add(new ProducesAttribute("application/json"));
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
