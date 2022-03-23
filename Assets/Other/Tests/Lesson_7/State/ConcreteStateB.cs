namespace BehavioralPatterns.State.ExampleFirst
{
    public sealed class ConcreteStateB : State
    {
        public override void Handle(Context context)
        {
            context.State = new ConcreteStateA();
        }
    }
}

