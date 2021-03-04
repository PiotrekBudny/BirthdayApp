using BirthdayTracker.Database;
using BirthdayTracker.Web.CsvParser;
using BirthdayTracker.Web.Providers;
using BirthdayTracker.Web.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BirthdayTracker.Web
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
            services
                .AddSingleton<IGetBirthdayPeopleDetailsResponseProvider, GetBirthdayPeopleDetailsResponseProvider>();
            services.AddSingleton<IAddBirthdayToListResponseProvider, AddBirthdayToListResponseProvider>();
            services.AddSingleton<IAddBirthdayHelper, AddBirthdayToCsvHelper>();

            services.AddSingleton<ICsvReaderWrapper, CsvReaderWrapper>();
            services.AddSingleton<ICsvWriterWrapper, CsvWriterWrapper>();
            services.AddSingleton<IBirthdayValidator, BirthdayValidator>();

            services.AddMvc(options => { options.Filters.Add(new ProducesAttribute("application/json")); });

            services.AddDbContext<BirthdayDbContext>();
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
