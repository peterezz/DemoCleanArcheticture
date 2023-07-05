using Clean_architecture.Application.Common.Interfaces;

namespace Clean_architecture.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
