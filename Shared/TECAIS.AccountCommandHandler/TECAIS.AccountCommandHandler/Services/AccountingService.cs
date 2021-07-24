using ModelContracts;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TECAIS.AccountCommandHandler.Services
{
    class AccountingService : IAccountingService
    {
        private readonly HttpClient _httpClient;

        public AccountingService(HttpClient client)
        {
            _httpClient = client;
        }
        public async Task<HttpStatusCode> UpdateAccount(AccountingCommand command)
        {
            try
            {
                Console.WriteLine("Updating account with new information");

              
                var content = new StringContent(JsonConvert.SerializeObject(command), Encoding.UTF8, "application/json");

                //HttpResponseMessage response = await _httpClient.PostAsync("https://heat_submission_service:80/HeatSubmission", content);
                HttpResponseMessage response = await _httpClient.PostAsync("http://swtdisp-grp10-accounting-service:80/api/Accounting", content); // for docker compose write: https://heat_submission_service:443/HeatSubmission
                response.EnsureSuccessStatusCode();

                //HttpRequestMessage message = new HttpRequestMessage();
                //message.RequestUri = new Uri("http://swtdisp-grp10-accounting-service:80/api/Accounting");
                //message.Content = new StringContent(jsonString);
                //message.Method = new HttpMethod("POST");

                //HttpResponseMessage response = await _httpClient.SendAsync(message);

                //response.EnsureSuccessStatusCode();

                

                return response.StatusCode;
            }
            catch (Exception e)
            {

                throw new Exception("UpdateAccount failed with: ",e);
            }
        }
    }
}
