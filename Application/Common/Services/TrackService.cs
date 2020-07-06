using System;
using System.Collections.Generic;
using Application.Common.Interfaces;
using Application.Common.Settings;
using Application.Conference.Composite;

namespace Application.Common.Services
{
    public class TrackService : ITrackService
    {
        private readonly ITimeService _timeService;

        public TrackService(ITimeService timeService)
        {
            _timeService = timeService;
        }

        public IList<ConferenceComponent> CalculateTalksForSession
            (IList<ConferenceComponent> allTalks, int maximumMinutes, int startingTime)
        {
            var totalLength = 0;
            var sessionTalks = new List<ConferenceComponent>();
            ConferenceComponent previousLeaf = null;

            foreach (var talk in allTalks)
                if (totalLength + talk.Duration <= maximumMinutes)
                {
                    totalLength += talk.Duration;

                    talk.TimeStamp = previousLeaf == null 
                        ? _timeService.CalculateStartingTimeStamp(startingTime) 
                        : _timeService.CalculateTimeStampFromPrevious(previousLeaf.TimeStamp, previousLeaf.Duration);

                    sessionTalks.Add(talk);
                    previousLeaf = talk;
                }

            return sessionTalks;
        }

        public IList<ConferenceComponent> RemoveSelectedTalksFromInputTalks
            (IList<ConferenceComponent> talks, IList<ConferenceComponent> talksToRemove)
        {
            foreach (var talk in talksToRemove) talks.Remove(talk);

            return talks;
        }

    }
}