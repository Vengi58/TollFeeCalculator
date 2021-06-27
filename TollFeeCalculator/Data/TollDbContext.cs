using Microsoft.EntityFrameworkCore;
using TollFeeCalculator.Model;

namespace TollFeeCalculator.Data
{
    public class TollDbContext : DbContext
    {
        public DbSet<TimeFrameFee> TimeFrameFees { get; set; }
        public DbSet<TollFreeDate> TollFreeDates { get; set; }
    }
}
