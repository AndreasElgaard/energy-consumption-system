using System;

namespace SagaContracts
{
    public interface HeatSubmissionCharged
    {
        Guid Id { get; }
        double PublicCharging { get; }
    }
}