using System;

namespace Application.Conference.Composite
{
    public abstract class ConferenceComponent
    {
        public string Name { get; set; }
        public int Duration { get; set; }
        public DateTime TimeStamp { get; set; }

        public abstract string Print();

        public virtual bool IsComposite()
        {
            return true;
        }

        // In leaf component => Violates ISP
        // Creates transparency (Composite & Leaf treated the same)
        public virtual void Add(ConferenceComponent component)
        {
            throw new NotImplementedException();
        }
    }
}