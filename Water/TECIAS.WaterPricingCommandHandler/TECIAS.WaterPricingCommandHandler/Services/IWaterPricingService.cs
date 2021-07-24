using System.Threading.Tasks;

namespace TECIAS.WaterPricingCommandHandler.Services
{
    public interface IWaterPricingService
    {
        Task<double> GetPrice();
    }
}
