using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TECAIS.IOT.ElectricMeteringUnit.Models;

namespace TECAIS.IOT.ElectricMeteringUnit.Services
{
    public class ElectricSubmissionService : IElectricSubmissionService
    {
        public readonly HttpClient _httpClient;

        public ElectricSubmissionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task PostHeatSubmission(ElectricSubmission submission)
        {

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            var content = new StringContent(JsonConvert.SerializeObject(submission), Encoding.UTF8, "application/json");

            // response = await _httpClient.PostAsync("https://electric_submission_service:443/ElectricSubmission", content);
            HttpResponseMessage response = await _httpClient.PostAsync("https://localhost:44325/ElectricitySubmission", content);

            response.EnsureSuccessStatusCode();
        }
    }
}
