using System.Threading.Tasks;
using TECAIS.IOT.ElectricMeteringUnit.Models;

namespace TECAIS.IOT.ElectricMeteringUnit.Services
{
    public interface IElectricSubmissionService
    {
        Task PostHeatSubmission(ElectricSubmission submission);
    }
}
