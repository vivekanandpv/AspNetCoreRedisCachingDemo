using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using AspNetCoreRedisCachingDemo.Data;
using AspNetCoreRedisCachingDemo.Models;
using AspNetCoreRedisCachingDemo.Services;

namespace AspNetCoreRedisCachingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IMemoryCacheService _redisCacheService;
        public SampleController(ICarService carProvider, IMemoryCacheService redisCacheService)
        {
            _carService = carProvider;
            _redisCacheService = redisCacheService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var inCache = _redisCacheService.Exists<Car>("CAR_IN_CACHE");

            if (inCache)
            {
                return Ok(_redisCacheService.Get<Car>("CAR_IN_CACHE"));
            }
            else
            {
                var car = _carService.Get();
                _redisCacheService.Set("CAR_IN_CACHE", car, TimeSpan.FromSeconds(30));
                return Ok(car);
            }
        }
    }
}
