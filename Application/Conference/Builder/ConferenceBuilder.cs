using System.Collections.Generic;
using Application.Common.Interfaces;
using Application.Common.Settings;
using Application.Conference.Composite;

namespace Application.Conference.Builder
{
    public class ConferenceBuilder : CompositeBuilder
    {
        private readonly Sessions _sessions;
        private readonly ITrackService _trackService;
        private IList<ConferenceComponent> _sessionTalks;

        public ConferenceBuilder(
            IList<ConferenceComponent> allTalks,
            ITrackService trackService,
            Sessions sessions) :
            base(allTalks)
        {
            _trackService = trackService;
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
                var finishEvent = _trackService.CalculateAfterSessionEvent(
                    _sessionTalks, sessionSettings.FinishingEvent, sessionSettings.MinStartEvent);

                track.Add(finishEvent);
            }

            return track;
        }

        private ConferenceComponent BuildSession(ref IList<ConferenceComponent> remainingTalks, int maxDuration,
            int startingTime)
        {
            var session = CreateComposite(null, maxDuration);

            _sessionTalks =
                _trackService.CalculateTalksForSession(remainingTalks, maxDuration, startingTime);
            remainingTalks = _trackService.RemoveSelectedTalksFromInputTalks(remainingTalks, _sessionTalks);

            foreach (var talk in _sessionTalks) session.Add(talk);

            return session;
        }
    }
}