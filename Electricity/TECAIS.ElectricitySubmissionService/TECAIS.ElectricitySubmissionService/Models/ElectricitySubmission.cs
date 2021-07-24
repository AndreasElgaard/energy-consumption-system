using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Saga
{
    public class ElectricitySubmission
    {
        public string Address { get; set; }
        public DateTime TimeOfMeasurement { get; set; }
        public float ElectricityConsumption { get; set; }
    }
}
