using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TECAIS.Pricing.ElectricityPricingService.Services;

namespace TECAIS.Pricing.ElectricityPricingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ElectricityPriceService : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public ElectricityPriceService(ICacheService cacheService)
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
