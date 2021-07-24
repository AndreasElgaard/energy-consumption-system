using Microsoft.AspNetCore.Mvc;
using TECAIS.PublicChargingService.Services;

namespace TECAIS.PublicChargingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicChargingController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public PublicChargingController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var price = _cacheService.GetCachedPrice();

            return Ok(price);
        }
    }
}
