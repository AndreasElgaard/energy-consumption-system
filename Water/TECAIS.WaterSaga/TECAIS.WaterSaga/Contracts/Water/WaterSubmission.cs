using System;
using System.Collections.Generic;
using System.Text;

namespace Saga
{
    public class WaterSubmission
    {
        public string Address { get; set; }
        public DateTime TimeOfMeasurement { get; set; }
        public float WaterConsumption { get; set; }
    }
}
