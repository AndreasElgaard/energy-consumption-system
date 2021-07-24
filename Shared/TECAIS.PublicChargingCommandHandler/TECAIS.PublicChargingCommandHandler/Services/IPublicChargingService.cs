using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TECAIS.PublicChargingCommandHandler.Services
{
    public interface IPublicChargingService
    {
        Task<double> GetPrice();
    }
}
