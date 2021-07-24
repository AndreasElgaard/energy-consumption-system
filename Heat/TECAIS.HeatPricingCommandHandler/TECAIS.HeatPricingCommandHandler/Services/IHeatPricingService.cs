using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECAIS.HeatPricingCommandHandler.Services
{
    public interface IHeatPricingService
    {
        Task<double> GetPrice();

    }
}
