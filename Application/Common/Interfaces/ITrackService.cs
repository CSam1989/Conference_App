using System.Collections.Generic;
using Application.Conference.Composite;

namespace Application.Common.Interfaces
{
    public interface ITrackService
    {
        IList<ConferenceComponent> CalculateTalksForSession
            (IList<ConferenceComponent> allTalks, int maximumMinutes);

        IList<ConferenceComponent> RemoveSelectedTalksFromInputTalks
            (IList<ConferenceComponent> Talks, IList<ConferenceComponent> TalksToRemove);
    }
}