using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging; // Add this line

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .Build();

host.Run();