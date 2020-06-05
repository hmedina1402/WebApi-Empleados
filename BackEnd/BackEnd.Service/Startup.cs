using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Util.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;

namespace BackEnd.Service
{
    public class Startup
    {
        public Startup(IConfiguration cnfs, IHostingEnvironment env)
        {
            /*Configuration = configuration;*/
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.AddInMemoryCollection(cnfs.AsEnumerable()).Build();
            Environment = env;
        }

        public IHostingEnvironment Environment { get; }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //CADENAS MOTOR DE BASE DE DATOS
            services.Configure<ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
            {
                Version = "v1",
                Title = "Test Canvia Company",
                Description = "Prueba de Postulante Henry Medina --> Control de Empleados",
                TermsOfService = null,
                Contact = new Microsoft.OpenApi.Models.OpenApiContact {
                    Name = "Henry Medina",
                    Email = "hmedina1402@gmail.com",
                    Url = null
                }
            }));

      


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IOptions<Swagger> swaggerOptions)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //SWAGGER
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                //c.SwaggerEndpoint($"{swaggerOptions.Value.BasePath}/swagger/{swaggerOptions.Value.Version}/swagger.json", swaggerOptions.Value.Title);
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Test Version 1");
            });


            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
