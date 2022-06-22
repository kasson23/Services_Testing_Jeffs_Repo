namespace OnCallDeveloperApi.Adapters
{
    public class SystemTime : ISystemTime
    {
        public DateTime GetCurrent()
        {
            return DateTime.Now;
        }
    }
}
