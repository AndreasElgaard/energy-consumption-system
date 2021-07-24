using System;
using System.Collections.Generic;
using System.Text;

namespace SagaContracts
{
    public interface WaterSubmissionsSubmitted
    {
        string CustomerAddress { get; }
        double WaterConsumption { get; }
        Guid Id { get; }
        DateTime TimeOfMeasurement { get; }
    }
}
