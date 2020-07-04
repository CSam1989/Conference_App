using System.Collections.Generic;

namespace Application.Common.Interfaces
{
    public interface IInputStrategy
    {
        IList<Talk> Read();
    }
}