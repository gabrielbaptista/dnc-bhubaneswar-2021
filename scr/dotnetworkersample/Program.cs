using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Azure.Functions.Worker.Configuration;
using static FunctionAppSample.ApiTriggerFunction;

namespace FunctionApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // You cannot debbug using VS 
            // You may run in command line (func host start --verbose) or
            // Follow instructions from https://github.com/Azure/azure-functions-dotnet-worker-preview
            // #if DEBUG
            //             Debugger.Launch();
            // #endif
            // Test API Function: http://localhost:7071/api/ApiTriggerFunction?name=[Your Name]
            var host = new HostBuilder()
                .ConfigureAppConfiguration(c =>
                {
                    c.AddCommandLine(args);
                })
                .ConfigureFunctionsWorker((c, b) =>
                {
                    b.UseFunctionExecutionMiddleware();
                })
                .ConfigureServices(s =>
                {
                    s.AddSingleton<IHttpResponderService, DefaultHttpResponderService>();
                })
                .Build();

            await host.RunAsync();
        }
    }
}