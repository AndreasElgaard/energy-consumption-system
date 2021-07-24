using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TECAIS.PublicChargingCommandHandler.Services
{
    class PublicChargingService : IPublicChargingService
    {
        private readonly HttpClient _httpClient;


        public PublicChargingService(HttpClient client)
        {
            _httpClient = client;
        }

        public async Task<double> GetPrice()
        {
            try
            {
                Console.WriteLine("Getting price from PublicChargingService");
                HttpResponseMessage response = await _httpClient.GetAsync("http://swtdisp-grp10-public-charging-service-service:80/api/PublicCharging");

                response.EnsureSuccessStatusCode();

                string reponseContent = await response.Content.ReadAsStringAsync();

                return double.Parse(reponseContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed with:");
                Console.WriteLine(ex.Message);
                throw new Exception("fuck");
            }
        }
    }
}
