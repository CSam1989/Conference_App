using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Conference.Composite
{
    public class ConferenceClient
    {
        public string Print(ConferenceComponent component)
        {
            return component.Print();
        }

        public void AddComponent(ConferenceComponent parentComponent, ConferenceComponent childComponent)
        {
            if(parentComponent.IsComposite())
                parentComponent.Add(childComponent);
        }
    }
}
