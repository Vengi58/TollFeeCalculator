namespace TollFeeCalculator.Model
{
    public class TimeFrameFee
    {
        public string Name { get; set; }
        public int StartHour { get; set; }
        public int StartMinute { get; set; }
        public int EndHour { get; set; }
        public int EndMinute { get; set; }
        public int Fee { get; set; }

    }
}
