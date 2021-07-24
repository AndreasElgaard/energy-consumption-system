namespace TECAIS.PublicChargingService.Services
{
    public interface ICacheService
    {
        double GetCachedPrice();
        void UpdateCachedPrice(double value);
        void ClearCache();
    }
}
