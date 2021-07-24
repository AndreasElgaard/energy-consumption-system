using System.Threading.Tasks;

namespace TECAIS.Pricing.HeatPricingService.Services
{
    public interface ICacheService
    {
        double GetCachedPrice();
        void UpdateCachedPrice(double value);
        void ClearCache();
    }
}
