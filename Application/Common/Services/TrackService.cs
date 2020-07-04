using System.Collections.Generic;
using Application.Common.Interfaces;
using Application.Common.Settings;
using Application.Conference.Composite;

namespace Application.Common.Services
{
    public class TrackService : ITrackService
    {
        private readonly SpecialLengthSettings _specialLength;

        public TrackService(SpecialLengthSettings specialLength)
        {
            _specialLength = specialLength;
        }

        public IList<ConferenceComponent> CalculateTalksForSession
            (IList<ConferenceComponent> allTalks, int maximumMinutes)
        {
            var totalLength = 0;
            var sessionTalks = new List<ConferenceComponent>();

            foreach (var talk in allTalks)
                if (totalLength + talk.Duration <= maximumMinutes)
                {
                    totalLength += talk.Duration;
                    sessionTalks.Add(talk);
                }

            return sessionTalks;
        }

        public IList<ConferenceComponent> RemoveSelectedTalksFromInputTalks
            (IList<ConferenceComponent> Talks, IList<ConferenceComponent> TalksToRemove)
        {
            foreach (var talk in TalksToRemove) Talks.Remove(talk);

            return Talks;
        }
    }
}