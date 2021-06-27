using System;
using System.Collections.Generic;

namespace TollFeeCalculator.Data
{
    public interface ITollRepository
    {
        public List<DateTime> GetTollFreeDatesForYear(int year);
        int GetFeeForDate(DateTime date);
        List<int> GetTollFreeMonths();
    }
}
