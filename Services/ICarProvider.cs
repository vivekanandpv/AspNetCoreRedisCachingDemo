using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AspNetCoreRedisCachingDemo.Models;

namespace AspNetCoreRedisCachingDemo.Data
{
    public interface ICarService
    {
        Car Get();
    }

    public class CarService : ICarService
    {
        public Car Get()
        {
            Thread.Sleep(3000);
            return new Car { Color = "Green", Id = 1234, Make = "Maruti Suzuki", Model = "Dzire", Year = 2020 };
        }
    }
}
