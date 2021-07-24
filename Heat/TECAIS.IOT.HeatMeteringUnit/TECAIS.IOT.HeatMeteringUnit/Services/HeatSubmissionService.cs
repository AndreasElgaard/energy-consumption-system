using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TECAIS.IOT.HeatMeteringUnit.Models;

namespace TECAIS.IOT.HeatMeteringUnit.Services
{
    public class HeatSubmissionService : IHeatSubmissionService
    {
        public readonly HttpClient _httpClient;

        public HeatSubmissionService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task PostHeatSubmission(HeatSubmission submission)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            var content = new StringContent(JsonConvert.SerializeObject(submission), Encoding.UTF8, "application/json");
            
            //HttpResponseMessage response = await _httpClient.PostAsync("https://heat_submission_service:80/HeatSubmission", content);
            HttpResponseMessage response = await _httpClient.PostAsync("http://swtdisp-grp10-heat-submission-service:80/HeatSubmission", content); // for docker compose write: https://heat_submission_service:443/HeatSubmission
            response.EnsureSuccessStatusCode();
        }

        public async Task PostStatusSubmission(StatusSubmission submission)
        {
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls13;
            var content = new StringContent(JsonConvert.SerializeObject(submission), Encoding.UTF8, "application/json");
            using (var _httpClient = new HttpClient())
            {
                //HttpResponseMessage response = await _httpClient.PostAsync("https://heat_status_service:443/StatusSubmission", content);
                HttpResponseMessage response = await _httpClient.PostAsync("http://swtdisp-grp10-heat-status-submission-service:80/StatusSubmission", content);
                response.EnsureSuccessStatusCode();
            }
        }
    }
}