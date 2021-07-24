using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TECAIS.Pricing.WaterPricingService.Services
{
    public interface ICacheService
    {
        double GetCachedPrice();
        void UpdateCachedPrice(double value);
        void ClearCache();
    }
}
