namespace SCADA.Model
{
    public class TimeRange
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public TimeRange() { }

        public TimeRange(DateTime start, DateTime end)
        {
            this.StartTime = start;
            this.EndTime = end; 
        }

    }
}
