using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TECAIS.HeatPricingCommandHandler.Services
{
    public class HeatPricingService : IHeatPricingService
    {
        private readonly HttpClient _htttpClient;
       // private readonly ILogger _logger;
        public HeatPricingService(HttpClient client)
        {
            _htttpClient = client;
           // _logger = logger;
        }
        public async Task<double> GetPrice()
        {
            try
            {
                Console.WriteLine("Getting price from HeatPricingService");
                HttpResponseMessage response = await _htttpClient.GetAsync("http://swtdisp-grp10-heat-pricing-service-service:80/api/HeatPrice");

                response.EnsureSuccessStatusCode();

                string reponseContent = await response.Content.ReadAsStringAsync();

                return double.Parse(reponseContent);
            }
            catch (Exception ex)
            {
                throw new Exception("GetPrice failed with:", ex);
            }
        }

        
    }
}
