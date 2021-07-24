using System.Threading.Tasks;
using TECAIS.IOT.WaterMeteringUnit.Models;

namespace TECAIS.IOT.WaterMeteringUnit.Services
{
    public interface IWaterSubmissionService
    {
        Task PostHeatSubmission(WaterSubmission submission);
    }
}
