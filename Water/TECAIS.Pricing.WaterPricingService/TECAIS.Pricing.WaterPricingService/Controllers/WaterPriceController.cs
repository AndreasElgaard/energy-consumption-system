using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TECAIS.Pricing.WaterPricingService.Services;

namespace TECAIS.Pricing.WaterPricingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WaterPriceController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public WaterPriceController(ICacheService cacheService)
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
