using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Conference.Composite
{
    public class ConferenceComposite: ConferenceComponent
    {
        private readonly IList<ConferenceComponent> _conferenceComponents;

        public ConferenceComposite(string name, int maxDuration)
        {
            Name = name;
            Duration = maxDuration;
            _conferenceComponents = new List<ConferenceComponent>();
        }

        public override void Add(ConferenceComponent component)
        {
            _conferenceComponents.Add(component);
        }

        public override string Print()
        {
            StringBuilder builder = new StringBuilder();
            if (Name!= null)
                builder.Append($"\n{Name}\n"); //newline before & after name

            foreach (var component in _conferenceComponents)
            {
                builder.Append(component.Print());
            }

            return builder.ToString();
        }
    }
}
