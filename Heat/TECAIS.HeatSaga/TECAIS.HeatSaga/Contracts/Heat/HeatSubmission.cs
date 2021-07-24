using System;

namespace Saga
{
    public class HeatSubmission
    {
        public string Address { get; set; }
        public DateTime TimeOfMeasurement { get; set; }
        public float HeatConsumption { get; set; }
    }
}
