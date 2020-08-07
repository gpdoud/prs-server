using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PrsServer.Models;

namespace prs_server {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private readonly string DefaultCorsPolicy = "_defaultCorsPolicy";
        private string[] AllowedOrigins = {
            "http://localhost", "https://localhost",
            "http://localhost:4200", "https://localhost:4200",
            "http://doudsystems.com", "https://doudsystems.com"
        };
        private string[] AllowedMethods = { "GET", "POST", "PUT", "DELETE", "OPTION" };

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            var connStrKey = "ProdDb";
#if DEBUG
            connStrKey = "DevDb";
#endif
            services.AddDbContext<PrsDbContext>(x => {
                x.UseLazyLoadingProxies();
                x.UseSqlServer(Configuration.GetConnectionString(connStrKey));
            });
            services.AddCors(x =>
                x.AddPolicy(DefaultCorsPolicy, x =>
                    x.WithOrigins(AllowedOrigins).WithMethods(AllowedMethods).AllowAnyHeader()
                )
            );
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseCors(DefaultCorsPolicy);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });

            using(var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope()) {
                scope.ServiceProvider.GetService<PrsDbContext>().Database.Migrate();
            }
        }
    }
}
