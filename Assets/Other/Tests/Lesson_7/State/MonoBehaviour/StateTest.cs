using UnityEngine;

namespace BehavioralPatterns.State.ExampleFirst
{
    public sealed class StateTest : MonoBehaviour
    {
        private void Start()
        {
            Context c = new Context(new ConcreteStateA());
            c.Request();
            c.Request();
            c.Request();
            c.Request();
        }
    }
}
