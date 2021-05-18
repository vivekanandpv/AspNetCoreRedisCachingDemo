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
        private readonly IRedisCacheService _redisCacheService;
        public SampleController(ICarService carProvider, IRedisCacheService redisCacheService)
        {
            _carService = carProvider;
            _redisCacheService = redisCacheService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var inCache = await _redisCacheService.ExistsAsync("CAR_IN_CACHE");

            if (inCache)
            {
                return Ok(await _redisCacheService.GetAsync<Car>("CAR_IN_CACHE"));
            }
            else
            {
                var car = _carService.Get();
                await _redisCacheService.SetAsync("CAR_IN_CACHE", car, TimeSpan.FromMinutes(1));
                return Ok(car);
            }
        }
    }
}
