using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SagaContracts
{
    public interface HeatSubmissionPriced
    {
        Guid Id { get; }
        double Price { get; }
    }
}
