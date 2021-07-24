using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaContracts
{
    public interface HeatSubmissionCharged
    {
        Guid Id { get; }
        double PublicCharging { get; }
    }
}
