using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Function
{
    public class MiddleTier
    {
        private readonly ILogger<MiddleTier> _logger;

        public MiddleTier(ILogger<MiddleTier> logger)
        {
            _logger = logger;
        }

        [Function("MiddleService")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Evento>(requestBody);

            _logger.LogInformation(requestBody);

            var client = new HttpClient();
            var lastServiceUrl = Environment.GetEnvironmentVariable("LAST_SERVICE_URL");
            var response = await client.PostAsync(lastServiceUrl, new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json"));

            return new OkObjectResult(requestBody);
        }
    }
}