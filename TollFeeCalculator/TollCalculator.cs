using System;
using System.Linq;
using System.Collections.Generic;
using TollFeeCalculator.Vehicles;
using TollFeeCalculator.Data;

public class TollCalculator
{
    readonly List<Type> TollFreeVehicleTypes = new() { typeof(Motorbike), typeof(Tractor), typeof(Emergency), typeof(Diplomat), typeof(Foreign), typeof(Military) };

    private readonly ITollRepository repository;

    public TollCalculator(ITollRepository tollRepository)
    {
        repository = tollRepository;
    }

    public int GetTollFee(DateTime[] dates, Vehicle vehicle)
    {
        if (IsTollFreeDate(dates[0]) || IsTollFreeVehicle(vehicle)) return 0;

        var lookupDates = dates.ToLookup(d => WithinAnHour(dates[0], d));
        var firstHourDates = lookupDates[true];
        var overAnHourDates = lookupDates[false];

        var firstHourFee = firstHourDates.Max(d => GetTollFee(d));
        var overAnHourFees = overAnHourDates.Sum(d => GetTollFee(d));

        return Math.Min(60, firstHourFee + overAnHourFees);
    }
    private bool WithinAnHour(DateTime start, DateTime end)
    {
        return end.Subtract(start) <= TimeSpan.FromMinutes(60);
    }

    private int GetTollFee(DateTime date)
    {
        return repository.GetFeeForDate(date);
    }

    private bool IsTollFreeVehicle(Vehicle vehicle)
    {
        if (vehicle == null) return false;
        return TollFreeVehicleTypes.Any(v => v.GetType().Equals(vehicle.GetType()));
    }

    private bool IsTollFreeDate(DateTime date)
    {
        return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday ||
            repository.GetTollFreeMonths().Contains(date.Month) ||
            repository.GetTollFreeDatesForYear(date.Year).Any(d => d.Year == date.Year && d.Month == date.Month && d.Day == date.Day);
    }
}