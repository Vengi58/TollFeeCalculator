using System;
using System.Collections.Generic;
using TollFeeCalculator.Data;
using TollFeeCalculator.Vehicles;

namespace TollFeeCalculator
{
    class Program
    {
        static void Main(string[] args)
        {
            var tollCalculator = new TollCalculator(new TollRepository(new TollDbContext()));
            var dates = new List<DateTime> {
                new DateTime(2013, 1, 12, 6, 15, 34),
                new DateTime(2013, 1, 12, 8, 22, 11),
                new DateTime(2013, 1, 12, 13, 31, 1),
                new DateTime(2013, 1, 12, 15, 11, 10),
                new DateTime(2013, 1, 12, 16, 25, 10),
            };
            var totalFee = tollCalculator.GetTollFee(dates.ToArray(), new Car());
        }
    }
}
