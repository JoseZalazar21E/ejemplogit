using DataAccess.Generic;
using Microsoft.EntityFrameworkCore;
using Entities.DataContext;
using ProyectoEncodeApi;
using Microsoft.AspNetCore.Hosting;
//using MySqlConnector;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        using (var scope = host.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            try
            {
                var context = serviceProvider.GetRequiredService<ApiDbContext>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al obtener el contexto: {ex.Message}");
            }
        }

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
        => Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
}


