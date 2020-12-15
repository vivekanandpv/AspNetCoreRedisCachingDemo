using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreRedisCachingDemo.Models;

namespace AspNetCoreRedisCachingDemo.Data
{
    public interface ICarProvider
    {
        Car Get();
    }

    public class CarProvider : ICarProvider
    {
        public Car Get()
        {
            Thread.Sleep(3000);
            return new Car { Color = "Green", Id = 1234, Make = "Maruti Suzuki", Model = "Dzire", Year = 2020 };
        }
    }
}
