using System;
using System.Collections.Generic;
using System.Linq;
using Application.Common.Interfaces;
using Application.Common.Settings;
using Application.Conference.Composite;

namespace Application.Common.Services
{
    public class TrackService : ITrackService
    {
        private readonly SpecialLengthSettings _specialLength;
        private readonly ITimeService _timeService;

        public TrackService(ITimeService timeService, SpecialLengthSettings specialLength)
        {
            _timeService = timeService;
            _specialLength = specialLength;
        }

        public IList<ConferenceComponent> CalculateTalksForSession
            (IList<ConferenceComponent> allTalks, int maximumMinutes, int startingTime)
        {
            if (allTalks == null)
                throw new ArgumentNullException();

            if (maximumMinutes <= 0 || startingTime < 0)
                throw new ArgumentOutOfRangeException();

            var totalLength = 0;
            var sessionTalks = new List<ConferenceComponent>();
            ConferenceComponent previousLeaf = null;

            foreach (var talk in allTalks)
                if (totalLength + talk.Duration <= maximumMinutes)
                {
                    totalLength += talk.Duration;

                    talk.TimeStamp = previousLeaf != null
                        ? _timeService.CalculateTimeStampFromPrevious(previousLeaf.TimeStamp, previousLeaf.Duration)
                        : _timeService.CalculateStartingTimeStamp(startingTime);

                    sessionTalks.Add(talk);
                    previousLeaf = talk;
                }

            return sessionTalks;
        }

        public ConferenceComponent CalculateAfterSessionEvent
            (IList<ConferenceComponent> sessionTalks, string name, int minStartEvent)
        {
            if (sessionTalks.Count == 0)
                return null;

            var sessionEvent = new ConferenceLeaf(name, 0, _specialLength);

            var lastTalk = sessionTalks.Last();

            sessionEvent.TimeStamp = lastTalk.TimeStamp.AddMinutes(lastTalk.Duration) >=
                                     DateTime.Today.AddHours(minStartEvent)
                ? _timeService.CalculateTimeStampFromPrevious(lastTalk.TimeStamp, lastTalk.Duration)
                : _timeService.CalculateStartingTimeStamp(minStartEvent);

            return sessionEvent;
        }

        public IList<ConferenceComponent> RemoveSelectedTalksFromInputTalks
            (IList<ConferenceComponent> talks, IList<ConferenceComponent> talksToRemove)
        {
            foreach (var talk in talksToRemove) talks.Remove(talk);

            return talks;
        }
    }
}