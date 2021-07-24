using System;
using System.Collections.Generic;
using System.Text;

namespace ModelContracts
{
    public interface ElectricitySubmission
    {
        public string Address { get; set; }
        public DateTime TimeOfMeasurement { get; set; }
        public float ElectricityConsumption { get; set; }
    }
}
