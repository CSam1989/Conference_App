
using System.Collections.Generic;

namespace Application.Common.Settings
{
    public class Sessions
    {
        public Sessions()
        {
            SessionList = new List<SessionSettings>();
        }

        public IList<SessionSettings> SessionList { get; set; }
    }

    public class SessionSettings
    {
        public string Name { get; set; }
        public int MaxLength { get; set; }
    }
}
