using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SagaContracts
{
    public class HeatSubmissionSubmitted
    {
        public Guid Id { get; set; }
        public string CustomerAddress { get; set; }
        public double HeatConsumption { get; set; }
        public DateTime TimeOfMeasurement { get; set; }
    }
}
