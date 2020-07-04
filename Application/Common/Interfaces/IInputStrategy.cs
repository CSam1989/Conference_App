using System.Collections.Generic;
using Application.Conference.Composite;

namespace Application.Common.Interfaces
{
    public interface IInputStrategy
    {
        IList<ConferenceComponent> Read();
    }
}