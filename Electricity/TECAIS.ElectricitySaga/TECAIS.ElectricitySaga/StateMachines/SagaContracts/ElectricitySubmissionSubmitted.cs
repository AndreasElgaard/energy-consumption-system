using System;
using System.Collections.Generic;
using System.Text;

namespace SagaContracts
{
    public interface ElectricitySubmissionSubmitted
    {
        string CustomerAddress { get; }
        double ElectricityConsumption { get; }
        Guid Id { get; }
        DateTime TimeOfMeasurement { get; }
    }
}
