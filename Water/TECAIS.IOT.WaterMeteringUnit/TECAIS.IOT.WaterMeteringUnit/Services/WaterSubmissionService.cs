using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using TECAIS.IOT.WaterMeteringUnit.Models;

namespace TECAIS.IOT.WaterMeteringUnit.Services
{
    public class WaterSubmissionService : IWaterSubmissionService
    {
        public readonly HttpClient _httpClient;

        public WaterSubmissionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task PostHeatSubmission(WaterSubmission submission)
        {

            var json = JsonSerializer.Serialize(submission);
            var content = new StringContent(json);

            HttpResponseMessage response = await _httpClient.PostAsync("url", content);
            response.EnsureSuccessStatusCode();
        }
    }
}
