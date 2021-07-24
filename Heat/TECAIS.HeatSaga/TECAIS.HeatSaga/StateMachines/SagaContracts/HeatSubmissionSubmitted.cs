using System;

namespace SagaContracts
{
    public interface HeatSubmissionSubmitted
    {
        string CustomerAddress { get; }
        double HeatConsumption { get; }
        Guid Id { get; }
        DateTime TimeOfMeasurement { get; }
    }
}