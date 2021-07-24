using System;
using System.Collections.Generic;
using System.Text;

namespace ModelContracts
{
    public interface AccountingCommand
    {
        public string ConsumptionType { get; set; }
        public string Address { get; set; }
        public DateTime TimeOfMeasurement { get; set; }
        public float Consumption { get; set; }
        public double Price { get; set; }
        public double TaxPrice { get; set; }
    }
}
