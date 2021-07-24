using System;

namespace SagaContracts
{
    public interface HeatSubmissionPriced
    {
        Guid Id { get; }
        double Price { get; }
    }
}