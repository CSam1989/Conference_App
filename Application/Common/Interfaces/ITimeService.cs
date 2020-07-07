using System;

namespace Application.Common.Interfaces
{
    public interface ITimeService
    {
        DateTime CalculateTimeStampFromPrevious(DateTime previousTimeStamp, int duration);
        DateTime CalculateStartingTimeStamp(int startingTime);
    }
}