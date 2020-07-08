namespace Application.Conference.Composite
{
    public class ConferenceClient
    {
        private readonly ConferenceComponent _component;

        public ConferenceClient(ConferenceComponent component)
        {
            _component = component;
        }

        public string Print()
        {
            return _component.Print();
        }
    }
}