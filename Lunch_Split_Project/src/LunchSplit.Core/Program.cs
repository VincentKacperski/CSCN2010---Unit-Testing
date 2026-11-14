using Castle.Core.Configuration;
using Square;

namespace SquarePaymentsApplication
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
        }
    }
}

//Still working on finishing th builder for getting clients
//I will be using this code to register gateway clients
//will register the access token later
/*
builder.Services.AddSingleton(ProviderMetadata =>
{
  var configuration = provider.GetRequiredService<IConfiguration>();
  var accessToken = configuration["Square:AccessToken"];
  return new SquareClient(accessToken);

});

builder.Services.AddScoped<ISquarePaymentsGateway, SquarePaymentsGateway>();

builder.Services.AddScoped<IPaymentProcessor, SquarePaymentService>();*/