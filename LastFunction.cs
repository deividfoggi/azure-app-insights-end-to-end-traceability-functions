using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public class LastFunction
    {
        private readonly ILogger<LastFunction> _logger;

        public LastFunction(ILogger<LastFunction> logger)
        {
            _logger = logger;
        }

        [Function("LastService")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Evento>(requestBody);

            _logger.LogInformation(requestBody);

            return new OkObjectResult(requestBody);
        }
    }
}
