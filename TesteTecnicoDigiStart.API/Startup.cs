using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Data.Common;
using System.Linq;
using TesteTecnicoDigiStart.Domain;
using TesteTecnicoDigiStart.Repository;

namespace TesteTecnicoDigiStart.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public DbConnection DbConnection => new SqlConnection(Configuration.GetConnectionString("DbAccess"));
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(
                    DbConnection,
                    assembly => assembly.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });

            DependencyInjection.Register(services);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TesteTecnicoDigiStart.API", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddPolicy("Total", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddMvc().ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList();
                    
                    var result = new StandardReturnDTO
                    {
                        status_code = "400",
                        message = "The request contains some errors.",
                        data = string.Join(", ", errors)
                    };
                    return new BadRequestObjectResult(result);
                };
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();                
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TesteTecnicoDigiStart.API v1"));
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("Total");
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
