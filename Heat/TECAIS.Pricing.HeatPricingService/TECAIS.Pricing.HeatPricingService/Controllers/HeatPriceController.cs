using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TECAIS.Pricing.HeatPricingService.Services;

namespace TECAIS.Pricing.HeatPricingService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeatPriceController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public HeatPriceController(ICacheService cacheService)
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
