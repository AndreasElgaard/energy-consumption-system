using System;
using System.Collections.Generic;
using System.Text;

namespace SagaContracts
{
    public interface ElectricitySubmissionCharged
    {
        Guid Id { get; }
        double PublicCharging { get; }
    }
}
