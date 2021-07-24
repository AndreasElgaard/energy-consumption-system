using System.Threading.Tasks;
using TECAIS.IOT.HeatMeteringUnit.Models;

namespace TECAIS.IOT.HeatMeteringUnit.Services
{
    public interface IHeatSubmissionService
    {
        Task PostHeatSubmission(HeatSubmission submission);
        Task PostStatusSubmission(StatusSubmission submission);

    }
}
