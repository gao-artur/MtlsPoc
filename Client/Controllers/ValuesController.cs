using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Client.Controllers
{
    [Route("client/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ValuesController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        [Route("get-with-certificate")]
        public async Task<ActionResult<string>> GetWithCertificate()
        {
            using (var client = _httpClientFactory.CreateClient("ServerWithCertificate"))
            {
                return await GetFromServer(client);
            }
        }

        [HttpGet]
        [Route("get-without-certificate")]
        public async Task<ActionResult<string>> GetWithoutCertificate()
        {
            using (var client = _httpClientFactory.CreateClient("Server"))
            {
                return await GetFromServer(client);
            }
        }

        private async Task<string> GetFromServer(HttpClient client)
        {
            using(var request = new HttpRequestMessage(HttpMethod.Get, "server/values"))
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}
