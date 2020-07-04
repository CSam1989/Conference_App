using System.Collections.Generic;
using Application.Conference.Composite;

namespace Application.Conference.Builder
{
    public abstract class CompositeBuilder
    {
        private readonly IList<ConferenceComponent> _allTalks;

        protected CompositeBuilder(IList<ConferenceComponent> allTalks)
        {
            _allTalks = allTalks;
        }

        public ConferenceClient BuildConference()
        {
            var conference = CreateComposite();

            var remainingTalks = _allTalks;
            var i = 1;

            while (remainingTalks.Count != 0)
            {
                conference.Add(BuildTrack(ref remainingTalks, $"Track {i}:"));
                i++;
            }

            return new ConferenceClient(conference);
        }

        public abstract ConferenceComponent CreateComposite(string name = null, int maxDuration = 0);
        public abstract ConferenceComponent BuildTrack(ref IList<ConferenceComponent> remainingTalks, string name);
    }
}