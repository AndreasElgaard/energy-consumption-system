using System;
using System.Collections.Generic;
using System.Text;

namespace SagaContracts
{
    public interface ElectricitySubmissionPriced
    {
        Guid Id { get; }
        double Price { get; }
    }
}
