using Core.Repositories.Contracts;
using Core.Repositories.Contracts.UnitOfWork;
using Core.Services.Contracts;
using Core.Services.Implementations;
using Infraestructure.Data.Contexts;
using Infraestructure.Data.Repositories;
using Infraestructure.Data.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FarmacityTest
{
    public class Startup
    {
        private const string ApiName = "Farmacity API";
        private const string ApiVersion = "v1";
        private const string CorsPolicyName = "CorsPolicy";
        private const string ConnectionStringName = "FarmacityTest";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Configuración de servicios
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<DbContext, Context>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductRepository, ProductRepository>();
            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = ApiName,
                    Version = ApiVersion
                });
            });

            // Controladores
            services.AddControllers();

            // Configuración de CORS
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });

            // Configuración de base de datos
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            // AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

      
        }

        // Configuración del middleware
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Farmacity API v1");
                    c.RoutePrefix = "swagger";
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(CorsPolicyName);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
