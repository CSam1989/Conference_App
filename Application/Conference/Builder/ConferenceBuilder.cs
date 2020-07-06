using System;
using System.Collections.Generic;
using Application.Common.Interfaces;
using Application.Common.Settings;
using Application.Conference.Composite;

namespace Application.Conference.Builder
{
    public class ConferenceBuilder : CompositeBuilder
    {
        private readonly IList<ConferenceComponent> _allTalks;
        private readonly Sessions _sessions;
        private readonly ITrackService _trackService;
        private readonly SpecialLengthSettings _specialLength;

        public ConferenceBuilder(
            IList<ConferenceComponent> allTalks, 
            ITrackService trackService,
            SpecialLengthSettings specialLength, 
            Sessions sessions) :
            base(allTalks)
        {
            _allTalks = allTalks;
            _trackService = trackService;
            _specialLength = specialLength;
            _sessions = sessions;
        }

        public override ConferenceComponent CreateComposite(string name = null, int maxDuration = 0)
        {
            return new ConferenceComposite(name, maxDuration);
        }

        public override ConferenceComponent BuildTrack(ref IList<ConferenceComponent> remainingTalks, string name)
        {
            var track = CreateComposite(name);

            foreach (var sessionSettings in _sessions.SessionList)
            {
                track.Add(BuildSession(ref remainingTalks, sessionSettings.MaxLength, sessionSettings.StartSession));
                track.Add(new ConferenceLeaf(sessionSettings.FinishingEvent, 0, _specialLength));
            }

            return track;
        }

        private ConferenceComponent BuildSession(ref IList<ConferenceComponent> remainingTalks, int maxDuration, int startingTime)
        {
            var session = CreateComposite(null, maxDuration);

            var sessionTalks =
                _trackService.CalculateTalksForSession(remainingTalks, maxDuration, startingTime);
            remainingTalks = _trackService.RemoveSelectedTalksFromInputTalks(remainingTalks, sessionTalks);

            foreach (var talk in sessionTalks) session.Add(talk);

            return session;
        }
    }
}