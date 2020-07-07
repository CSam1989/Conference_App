using System.Collections.Generic;
using Application.Common.Settings;
using Application.Conference.Composite;

namespace Application.Common.Interfaces
{
    public interface ITrackService
    {
        IList<ConferenceComponent> CalculateTalksForSession
            (IList<ConferenceComponent> allTalks, int maximumMinutes, int startingTime);

        ConferenceComponent CalculateAfterSessionEvent
            (IList<ConferenceComponent> sessionTalks, string name, int minStartEvent, SpecialLengthSettings specialLength);

        IList<ConferenceComponent> RemoveSelectedTalksFromInputTalks
            (IList<ConferenceComponent> talks, IList<ConferenceComponent> talksToRemove);
    }
}