using BlogAppWASM.Shared;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlogAppWASM.Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            //call Blog API methods
            builder.Services.AddScoped(sp => new HttpClient{
                    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)});
            builder.Services.AddScoped<IBlogRepository, ClientRepository>();

            await builder.Build().RunAsync();
        }
    }
}
