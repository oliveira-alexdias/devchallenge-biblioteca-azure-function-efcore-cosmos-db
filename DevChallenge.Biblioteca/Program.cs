using System.IO;
using Microsoft.Azure.Functions.Worker.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using DevChallenge.Biblioteca.Data.Repository;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace DevChallenge.Biblioteca
{
    public class Program
    {
        public static Task Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IObraRepository, ObraRepository>();
                })
                .ConfigureOpenApi()
                .ConfigureAppConfiguration(builder =>
                {
                    builder.AddJsonFile("local.settings.json", optional: true, reloadOnChange: false);
                })
                .Build();

            return host.RunAsync();
        }
    }
}