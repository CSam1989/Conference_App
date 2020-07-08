using System.Collections.Generic;
using System.Text;

namespace Application.Conference.Composite
{
    public class ConferenceComposite : ConferenceComponent
    {
        public ConferenceComposite(string name = null, int maxDuration = 0)
        {
            Name = name;
            Duration = maxDuration;
            ConferenceComponents = new List<ConferenceComponent>();
        }

        public IList<ConferenceComponent> ConferenceComponents { get; }

        public override void Add(ConferenceComponent component)
        {
            ConferenceComponents.Add(component);
        }

        public override string Print()
        {
            var builder = new StringBuilder();
            if (Name != null)
                builder.Append($"\n{Name}\n"); //newline before & after name

            foreach (var component in ConferenceComponents)
                    builder.Append(component?.Print());

            return builder.ToString();
        }
    }
}