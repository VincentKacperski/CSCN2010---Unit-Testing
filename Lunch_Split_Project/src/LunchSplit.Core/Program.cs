using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Square;

namespace SquarePaymentsApplication
{
    //Create a new class the represents the program
    public class Program
    {
        //Create a new main method that returns a new task and takes command line arguments if needed
        public static async Task Main(string[] args)
        {
            //Creates a host builder that prepres a solid application environment
            /* 
            -- Gives access to appsettings.json and other configuration settings
            -- Provices dependancy injections
            -- Manages the life time of the application
            -- Provides hosting servies
            */
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddSingleton(provider =>
            {
                IConfiguration configuration = provider.GetRequiredService<IConfiguration>();
                var accessToken = configuration["Square:AccessToken"] ?? throw new InvalidOperationException("Missing Token!");
                return new SquareClient(accessToken);
            });
            
            //On instance can be processed per scoped 
            builder.Services.AddScoped<ISquarePaymentsGateway, SquarePaymentsGateway>();
            builder.Services.AddScoped<IPaymentProcessor, SquarePaymentService>();

            var host = builder.Build();
            await host.RunAsync();

        }
    }
}

