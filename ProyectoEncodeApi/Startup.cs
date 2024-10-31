using Entities.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProyectoEncodeApi.Middleware;

namespace ProyectoEncodeApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            var ConeccionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApiDbContext>(options => options.UseMySql(ConeccionString, ServerVersion.AutoDetect(ConeccionString)));
            services.AddAuthorization();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mi API", Version = "v1" });
            });

            IoC.AddDependencia(services);
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiCRUD Encode");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
