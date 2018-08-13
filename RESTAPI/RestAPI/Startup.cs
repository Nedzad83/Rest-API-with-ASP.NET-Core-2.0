using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using NSwag.AspNetCore;
using RestAPI.Data;
using RestAPI.Data.Repositories;
using RestAPI.Data.Services;
using System;
using System.IO;
using System.Reflection;


namespace RestAPI
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
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<ProductsRepository>();
            services.AddScoped<EnvironmentMockupRepository>();
            services.AddScoped<EnvironmentRepository>();
            services.AddScoped(x => { return new AttributeService(Configuration.GetConnectionString("DefaultConnection"), new AttributeRepository(Configuration.GetConnectionString("DefaultConnection"))); });


            services.AddDbContext<ProductContext>(opt =>
                opt.UseInMemoryDatabase("ProductInventory"));

            services.AddDbContext<EnvironmentContext>(opt =>
               opt.UseInMemoryDatabase("EnvironmentInventory"));

            services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin()
                                                                    .AllowAnyMethod()
                                                                     .AllowAnyHeader()));
            services.AddMvc();
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("AllowAll");

            // Enable the Swagger UI middleware and the Swagger generator
            app.UseSwaggerUi(typeof(Startup).GetTypeInfo().Assembly, settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling = PropertyNameHandling.CamelCase;
            });

            app.UseMvc();
        }
    }

}
