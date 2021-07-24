using System;
using System.Collections.Generic;
using System.Text;

namespace ModelContracts
{
    public interface PublicChargingCommand
    {
        public double TaxPrice { get; set; }
    }
}
