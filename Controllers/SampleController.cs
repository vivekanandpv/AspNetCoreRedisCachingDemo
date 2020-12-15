using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCoreRedisCachingDemo.Data;
using AspNetCoreRedisCachingDemo.Models;
using EasyCaching.Core;

namespace AspNetCoreRedisCachingDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ICarProvider _carProvider;
        private readonly IEasyCachingProvider _cachingProvider;
        public SampleController(IEasyCachingProviderFactory cachingProviderFactory, ICarProvider carProvider)
        {
            _carProvider = carProvider;
            _cachingProvider = cachingProviderFactory.GetCachingProvider("DefaultRedis");
        }

        [HttpGet]
        public IActionResult Get()
        {
            var inCache = _cachingProvider.Exists("CAR_IN_CACHE");

            if (inCache)
            {
                return Ok(_cachingProvider.Get<Car>("CAR_IN_CACHE").Value);
            }
            else
            {
                var carFromProvider = _carProvider.Get();
                _cachingProvider.TrySet("CAR_IN_CACHE", carFromProvider, TimeSpan.FromMinutes(1));
                return Ok(carFromProvider);
            }
        }
    }
}
