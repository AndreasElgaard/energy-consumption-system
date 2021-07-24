using System;
using System.Collections.Generic;
using System.Text;

namespace SagaContracts
{
    public interface WaterSubmissionCharged
    {
        Guid Id { get; }
        double PublicCharging { get; }
    }
}
