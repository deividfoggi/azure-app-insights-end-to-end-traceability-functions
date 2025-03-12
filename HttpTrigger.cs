using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace Company.Function
{
    public class HttpTrigger
    {
        private readonly ILogger<HttpTrigger> _logger;

        public HttpTrigger(ILogger<HttpTrigger> logger)
        {
            _logger = logger;
        }

        [Function("HttpTrigger")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var data = JsonConvert.DeserializeObject<Evento>(requestBody);

            _logger.LogInformation(requestBody);

            // send a POST to /api/MiddleTier with the data as the body
            var client = new HttpClient();
            var middleTierUrl = Environment.GetEnvironmentVariable("MIDDLE_SERVICE_URL");
            var response = await client.PostAsync(middleTierUrl, new StringContent(requestBody, System.Text.Encoding.UTF8, "application/json"));
            var responseContent = await response.Content.ReadAsStringAsync();
            
            return new OkObjectResult(requestBody);

        }
    }

    public class Evento
    {
        public string id { get; set; }
        public string message { get; set; }
    }
}