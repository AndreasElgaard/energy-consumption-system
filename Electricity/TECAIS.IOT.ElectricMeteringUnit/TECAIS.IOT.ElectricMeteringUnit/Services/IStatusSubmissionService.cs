using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECAIS.IOT.ElectricMeteringUnit.Models;

namespace TECAIS.IOT.ElectricMeteringUnit.Services
{
    public interface IStatusSubmissionService
    {
        Task PostStatusSubmission(StatusSubmission submission);

    }
}
