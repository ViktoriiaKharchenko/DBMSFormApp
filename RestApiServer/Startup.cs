using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatabaseControl;
using System.IO;

namespace RestApiServer
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
            string path = Directory.GetParent(Environment.CurrentDirectory).FullName + "\\Database";
            services.Add(new ServiceDescriptor(typeof(DatabaseSystem), new DatabaseSystem(path)));
            services.AddControllers().AddNewtonsoftJson();
            //services.AddRazorPages();
            services.AddHttpContextAccessor();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "DatabaseControl.WebApi",
                    Version = "v1",
                    Description = "Code of the API goes here.",
                    Contact = new OpenApiContact
                    {
                        Name = "Viktoria Kharchenko",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/ViktoriiaKharchenko/DBMSFormApp/tree/master/RestApiServer"),
                    },
                });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "RestApiServer.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapRazorPages();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DatabaseControl.WebApi v1");

                c.RoutePrefix = "swagger/ui";
            });
        }
    }
}
