using System;
using System.Collections.Generic;
using System.Text;

namespace ModelContracts
{
    public interface ElectricityPriceCommand
    {
        public double ElectricityConsumption { get; set; }
    }
}
