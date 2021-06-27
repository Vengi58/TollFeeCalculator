using System;
using System.Collections.Generic;
using System.Linq;
using TollFeeCalculator.Model;

namespace TollFeeCalculator.Data
{
    public class TollRepository : ITollRepository
    {
        //These static lists ideally should come from a DB, using the TollDbContext
        //For simpliciy the TollRepository uses these static lists.
        private static List<TimeFrameFee> fees = new()
        {
            new TimeFrameFee { Name = "MorningLow", StartHour = 6, StartMinute = 0, EndHour = 6, EndMinute = 29, Fee = 8 },
            new TimeFrameFee { Name = "MorningMid", StartHour = 6, StartMinute = 30, EndHour = 6, EndMinute = 59, Fee = 13 },
            new TimeFrameFee { Name = "MorningHigh", StartHour = 7, StartMinute = 0, EndHour = 7, EndMinute = 59, Fee = 18 },
            new TimeFrameFee { Name = "DayTimeMid", StartHour = 8, StartMinute = 0, EndHour = 8, EndMinute = 29, Fee = 13 },
            new TimeFrameFee { Name = "DayTimeLow", StartHour = 8, StartMinute = 30, EndHour = 14, EndMinute = 59, Fee = 8 },
            new TimeFrameFee { Name = "AfternoonMid", StartHour = 15, StartMinute = 0, EndHour = 15, EndMinute = 29, Fee = 13 },
            new TimeFrameFee { Name = "AfternoonHigh", StartHour = 15, StartMinute = 30, EndHour = 16, EndMinute = 59, Fee = 18 },
            new TimeFrameFee { Name = "EveningMid", StartHour = 17, StartMinute = 0, EndHour = 17, EndMinute = 59, Fee = 13 },
            new TimeFrameFee { Name = "EveningLow", StartHour = 18, StartMinute = 0, EndHour = 18, EndMinute = 29, Fee = 8 },
            new TimeFrameFee { Name = "Free", StartHour = 18, StartMinute = 30, EndHour = 5, EndMinute = 59, Fee = 0 }
        };

        //This could be either a table of dates in the db or a table generated from TollFreeDates  with EF
        private static List<DateTime> tollFreeDates = new()
        {
            new DateTime(2013, 1, 1),
            new DateTime(2013, 3, 28), new DateTime(2013, 3, 29),
            new DateTime(2013, 4, 1), new DateTime(2013, 4, 30),
            new DateTime(2013, 5, 1), new DateTime(2013, 5, 8), new DateTime(2013, 5, 9),
            new DateTime(2013, 6, 5), new DateTime(2013, 6, 6), new DateTime(2013, 6, 21),
            new DateTime(2013, 11, 1),
            new DateTime(2013, 12, 24), new DateTime(2013, 12, 25), new DateTime(2013, 12, 26), new DateTime(2013, 12, 31)
        };

        private static List<int> tollFreeMonths = new() { 7 };

        public TollRepository(TollDbContext tollDbContext)
        {
            //tollDbContext should be used to connect to a database, for example using EntiyFramework. This code is meant to be simpler and so does not include EF
        }

        public int GetFeeForDate(DateTime date)
        {
            var timeFrameFee = fees.FirstOrDefault(f => f.StartHour <= date.Hour && f.StartMinute <= date.Minute && f.EndHour >= date.Hour && f.EndMinute >= date.Minute);
            return timeFrameFee != null ? timeFrameFee.Fee : 0;
        }

        public List<int> GetTollFreeMonths()
        {
            return tollFreeMonths;
        }

        public List<DateTime> GetTollFreeDatesForYear(int year)
        {
            return tollFreeDates.Where(f => f.Year == year).ToList();
        }
    }
}
