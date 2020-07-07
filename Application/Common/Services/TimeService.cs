using System;
using System.Collections.Generic;
using System.Text;
using Application.Common.Interfaces;

namespace Application.Common.Services
{
    public class TimeService : ITimeService
    {
        public DateTime CalculateTimeStampFromPrevious(DateTime previousTimeStamp, int duration)
        {
            return previousTimeStamp.AddMinutes(duration);
        }

        public DateTime CalculateStartingTimeStamp(int startingTime)
        {
            return DateTime.Today.AddHours(startingTime);
        }
    }
}
