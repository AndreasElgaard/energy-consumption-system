using Serilog;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace TECIAS.WaterPricingCommandHandler.Services
{
    public class WaterPricingService : IWaterPricingService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger _logger;

        public WaterPricingService(HttpClient httpClient, ILogger logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<double> GetPrice()
        {
            try
            {
                _logger.Information("Getting price from WaterPricingService");

                HttpResponseMessage response = await _httpClient.GetAsync("blablabal");
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                return double.Parse(responseBody);
            }
            catch (Exception e)
            {
                throw new Exception("GetPrice Failed", e);
            }
        }
    }
}
