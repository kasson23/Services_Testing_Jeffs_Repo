

namespace OnCallDeveloperApi.Domain;

public class DeveloperLookup
{
    private readonly IProvideTheBusinessClock _businessClock;

    public DeveloperLookup(IProvideTheBusinessClock businessClock)
    {
        _businessClock = businessClock;
    }

    public OnCallDeveloperResponse GetOnCallDeveloper()
    {
        if (_businessClock.IsBusinessHours())
        {
            var response = new OnCallDeveloperResponse
            {
                Name = "Sue Jones",
                Email = "sue@aol.com",
                Phone = "555-1212"
            };
            return response;
        } else
        {
            var response = new OnCallDeveloperResponse
            {
                Name = "Bill Smith",
                Email = "bill@contractor.com",
                Phone = "555-8888"
            };
            return response;
        }
    }
}
