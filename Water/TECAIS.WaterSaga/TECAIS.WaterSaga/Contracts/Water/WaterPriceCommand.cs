using System;
using System.Collections.Generic;
using System.Text;

namespace ModelContracts
{
    public interface WaterPriceCommand
    {
        public double WaterConsumption { get; set; }
    }
}
